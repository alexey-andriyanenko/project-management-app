#!/bin/bash
set -e

# Fetch environment variables from AWS Systems Manager Parameter Store
export AWS_DEFAULT_REGION=${AWS_DEFAULT_REGION:-eu-north-1}

echo "Fetching environment variables from Parameter Store..."

# Function to get parameter value
get_param() {
  aws ssm get-parameter --name "/project-management/$1" --query 'Parameter.Value' --output text 2>/dev/null || echo ""
}

# Function to get secure parameter value
get_secure_param() {
  aws ssm get-parameter --name "/project-management/$1" --with-decryption --query 'Parameter.Value' --output text 2>/dev/null || echo ""
}

# Load environment variables from Parameter Store
export DATABASE_HOST=$(get_param "DATABASE_HOST")
export DATABASE_PORT=$(get_param "DATABASE_PORT")
export DATABASE_NAME=$(get_param "DATABASE_NAME")
export DATABASE_USER=$(get_param "DATABASE_USER")
export DATABASE_PASSWORD=$(get_secure_param "DATABASE_PASSWORD")
export ASPNETCORE_ENVIRONMENT=$(get_param "ASPNETCORE_ENVIRONMENT")

echo "Environment variables loaded"

# Install EF tools if not present
dotnet tool install --global dotnet-ef --version 10.0.0 2>/dev/null || true
export PATH="$PATH:/root/.dotnet/tools"

# Change to backend directory
cd /var/www/backend

# Find and run migrations for each DataAccess assembly
echo "Looking for DataAccess assemblies..."
for dll in *DataAccess.dll; do
  if [ -f "$dll" ]; then
    echo "Running migrations for $dll..."
    dotnet ef database update --assembly "$dll" --verbose || echo "No migrations or failed for $dll, continuing..."
  fi
done

echo "Migration process complete"