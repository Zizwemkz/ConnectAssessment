# Use official ASP.NET runtime image for .NET 6
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copy csproj and restore as distinct layers
COPY ["ConnectAssessment.csproj", "./"]
RUN dotnet restore "./ConnectAssessment.csproj"

# Copy everything else and build
COPY . .
RUN dotnet build "./ConnectAssessment.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "./ConnectAssessment.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ConnectAssessment.dll"]