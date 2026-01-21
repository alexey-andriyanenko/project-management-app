#!/bin/bash
set -eo pipefail

# Get the script directory and navigate to repo root
SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
REPO_ROOT="$(cd "$SCRIPT_DIR/.." && pwd)"

# --- MAIN ---
echo "========================================="
echo "Starting Local Migration Process"
echo "========================================="
echo "Repository: $REPO_ROOT"
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

echo ""
echo "NOTE: Using connection strings from appsettings.Development.json"
echo ""

# Process each DataAccess project
PROCESSED_COUNT=0
FAILED_PROJECTS=()

for project_file in "${PROJECT_FILES[@]}"; do
    project_dir=$(dirname "$project_file")
    project_name=$(basename "$project_dir")
    
    echo "========================================="
    echo "Processing: $project_name"
    echo "========================================="
    echo "Project path: $project_dir"
    
    # Run migrations
    cd "$project_dir" || {
        echo "ERROR: Failed to change directory to $project_dir" >&2
        FAILED_PROJECTS+=("$project_name")
        echo ""
        continue
    }
    
    echo "Executing: dotnet ef database update"
    if dotnet ef database update 2>&1; then
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
