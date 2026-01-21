#!/bin/bash
set -eo pipefail

# --- CONFIG ---
AWS_REGION=${AWS_REGION:-}
AWS_SECRETS_MANAGER_SECRET_NAME=${AWS_SECRETS_MANAGER_SECRET_NAME:-}

# Validate required environment variables
if [ -z "$AWS_REGION" ]; then
    echo "ERROR: AWS_REGION environment variable is required" >&2
    exit 1
fi

if [ -z "$AWS_SECRETS_MANAGER_SECRET_NAME" ]; then
    echo "ERROR: AWS_SECRETS_MANAGER_SECRET_NAME environment variable is required" >&2
    exit 1
fi

# Get the script directory and navigate to repo root
SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
REPO_ROOT="$(cd "$SCRIPT_DIR/.." && pwd)"

# Fetch secrets once and cache them
SECRET_JSON=""

# --- FUNCTIONS ---
# Fetch all secrets from AWS Secrets Manager (called once)
fetch_all_secrets() {
    echo "Fetching secrets from AWS Secrets Manager..." >&2
    
    SECRET_JSON=$(aws secretsmanager get-secret-value \
        --region "$AWS_REGION" \
        --secret-id "$AWS_SECRETS_MANAGER_SECRET_NAME" \
        --query "SecretString" \
        --output text 2>&1)
    
    if [ $? -ne 0 ]; then
        echo "ERROR: Failed to fetch secret from AWS Secrets Manager" >&2
        echo "$SECRET_JSON" >&2
        exit 1
    fi
    
    echo "Secrets retrieved successfully" >&2
}

# Get connection string for a specific key from cached secrets
get_connection_string() {
    local key="$1"
    
    # Extract the value for the key
    local conn_str
    conn_str=$(echo "$SECRET_JSON" | \
        grep -o "\"$key\"[[:space:]]*:[[:space:]]*\"[^\"]*\"" | \
        sed -E 's/^"[^"]+"\s*:\s*"//; s/"$//')
    
    echo "$conn_str"
}

# --- MAIN ---
echo "========================================="
echo "Starting Migration Process"
echo "========================================="
echo "Repository: $REPO_ROOT"
echo "AWS Region: $AWS_REGION"
echo "Secret Name: $AWS_SECRETS_MANAGER_SECRET_NAME"
echo ""

# Install EF tools if not already installed
echo "Ensuring dotnet-ef tools are installed..."
if ! dotnet tool list --global | grep -q dotnet-ef; then
    echo "Installing dotnet-ef..."
    dotnet tool install --global dotnet-ef
else
    echo "dotnet-ef is already installed"
fi
export PATH="$PATH:$HOME/.dotnet/tools"

# Fetch all secrets once
fetch_all_secrets

# Find all DataAccess projects recursively
echo ""
echo "Scanning for DataAccess projects..."
mapfile -t PROJECT_FILES < <(find "$REPO_ROOT" -name "*.DataAccess.csproj" -type f)

if [ ${#PROJECT_FILES[@]} -eq 0 ]; then
    echo "ERROR: No DataAccess projects found in $REPO_ROOT" >&2
    exit 1
fi

echo "Found ${#PROJECT_FILES[@]} DataAccess project(s):"
for project_file in "${PROJECT_FILES[@]}"; do
    project_dir=$(dirname "$project_file")
    project_name=$(basename "$project_dir")
    echo "  - $project_name"
done

# Process each DataAccess project
echo ""
PROCESSED_COUNT=0
FAILED_PROJECTS=()

for project_file in "${PROJECT_FILES[@]}"; do
    project_dir=$(dirname "$project_file")
    project_name=$(basename "$project_dir")
    
    echo "========================================="
    echo "Processing: $project_name"
    echo "========================================="
    
    # Extract module name (e.g., "Tenant.DataAccess" -> "Tenant")
    MODULE_NAME=$(echo "$project_name" | sed 's/\.DataAccess$//')
    
    # Construct connection string key (e.g., "TenantManagementDb")
    CONN_KEY="${MODULE_NAME}ManagementDb"
    
    echo "Module: $MODULE_NAME"
    echo "Connection Key: $CONN_KEY"
    
    # Get connection string from cached secrets
    CONN_STR=$(get_connection_string "$CONN_KEY")
    
    if [ -z "$CONN_STR" ]; then
        echo "WARNING: Connection string for key '$CONN_KEY' not found in secrets, skipping $project_name" >&2
        FAILED_PROJECTS+=("$project_name")
        echo ""
        continue
    fi
    
    echo "Connection string found: [REDACTED]"
    echo "Project path: $project_dir"
    
    # Add SSL Mode to connection string if not present (required for PostgreSQL)
    if [[ ! "$CONN_STR" =~ [Ss][Ss][Ll] ]] && [[ ! "$CONN_STR" =~ [Ee]ncrypt ]]; then
        echo "Adding SSL Mode=Require to connection string..."
        CONN_STR="${CONN_STR};SSL Mode=Require"
    fi
    
    # Run migrations
    cd "$project_dir" || {
        echo "ERROR: Failed to change directory to $project_dir" >&2
        FAILED_PROJECTS+=("$project_name")
        echo ""
        continue
    }
    
    echo "Executing: dotnet ef database update --connection [REDACTED] --verbose"
    if dotnet ef database update --connection "$CONN_STR" --verbose 2>&1; then
        echo "✓ Successfully completed migrations for $project_name"
        PROCESSED_COUNT=$((PROCESSED_COUNT + 1))
    else
        echo "ERROR: Migration failed for $project_name" >&2
        FAILED_PROJECTS+=("$project_name")
    fi
    
    echo ""
done

# Summary
echo "========================================="
echo "Migration Summary"
echo "========================================="
echo "Total projects found: ${#PROJECT_FILES[@]}"
echo "Successfully migrated: $PROCESSED_COUNT"

if [ ${#FAILED_PROJECTS[@]} -gt 0 ]; then
    echo "Failed/Skipped projects: ${#FAILED_PROJECTS[@]}"
    for failed in "${FAILED_PROJECTS[@]}"; do
        echo "  - $failed"
    done
    exit 1
fi

echo ""
echo "✓ All migrations completed successfully!"
