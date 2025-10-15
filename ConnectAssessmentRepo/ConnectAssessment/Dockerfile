# Use official ASP.NET runtime for .NET 9
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy solution and project files first (to leverage Docker layer caching)
COPY ConnectAssessmentRepo/ ./src
# COPY ConnectAssessmentRepo/ConnectAssessment/*.csproj ConnectAssessment/
# COPY ConnectAssessmentRepo/ConnectAssessment.Common/*.csproj ConnectAssessment.Common/
# COPY ConnectAssessmentRepo/ConnectAssessment.Data/*.csproj ConnectAssessment.Data/
# COPY ConnectAssessmentRepo/ConnectAssessment.Repositories/*.csproj ConnectAssessment.Repositories/
# COPY ConnectAssessmentRepo/ConnectAssessment.Service/*.csproj ConnectAssessment.Service/
# COPY ConnectAssessmentRepo/ConnectAssessment.Tests/*.csproj ConnectAssessment.Tests/
# COPY ConnectAssessmentRepo/ConnectAssessment.sln ./

# Restore dependencies
RUN dotnet restore ./src/ConnectAssessmentRepo/ConnectAssessment/ConnectAssessment.sln

# Copy the rest of the source
COPY ConnectAssessmentRepo/ ./

# Build the main app
WORKDIR /src/ConnectAssessment
RUN dotnet build -c Release -o /app/build

# Publish stage
FROM build AS publish
WORKDIR /src/ConnectAssessment
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ConnectAssessment.dll"]
