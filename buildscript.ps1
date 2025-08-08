#!/bin/bash

set -e

# Restore dependencies
dotnet restore

# Build the solution in Release mode
dotnet build --configuration Release --no-restore

# Run all unit tests (will automatically discover test projects)
dotnet test --no-build --verbosity normal

echo "Build and tests completed successfully."