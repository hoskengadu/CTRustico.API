# See https://docs.microsoft.com/dotnet/core/docker/building-net-docker-images for .NET best practices
# Use the official .NET SDK image for build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and restore as distinct layers
COPY CTRustico.sln ./
COPY Academia.Api/Academia.Api.csproj Academia.Api/
COPY Academia.Domain/Academia.Domain.csproj Academia.Domain/
COPY Academia.Infrastructure/Academia.Infrastructure.csproj Academia.Infrastructure/
COPY Academia.Tests/Academia.Tests.csproj Academia.Tests/
RUN dotnet restore CTRustico.sln

# Copy everything else and build
COPY . .
WORKDIR /src/Academia.Api
RUN dotnet publish -c Release -o /app/publish --no-restore

# Use the official ASP.NET runtime image for the final stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Expose port 80
EXPOSE 80

# Set environment variables (optional, adjust as needed)
# ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "Academia.Api.dll"]
