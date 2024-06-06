# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["BloggingSystem.API/BloggingSystem.API.csproj", "BloggingSystem.API/"]
COPY ["BloggingSystem.Application/BloggingSystem.Application.csproj", "BloggingSystem.Application/"]
COPY ["BloggingSystem.Domain/BloggingSystem.Domain.csproj", "BloggingSystem.Domain/"]
COPY ["BloggingSystem.Infrastructure/BloggingSystem.Infrastructure.csproj", "BloggingSystem.Infrastructure/"]
RUN dotnet restore "BloggingSystem.API/BloggingSystem.API.csproj"
COPY . .
WORKDIR "/src/BloggingSystem.API"
RUN dotnet build "BloggingSystem.API.csproj" -c Release -o /app/build

# Stage 2: Publish the application
FROM build AS publish
RUN dotnet publish "BloggingSystem.API.csproj" -c Release -o /app/publish

# Stage 3: Build the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BloggingSystem.API.dll"]
