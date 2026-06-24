# Stage 1: Build and compile the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy the solution file
COPY CartingService.sln ./

# Copy all .csproj files from Carting layers
COPY CartingService.WebApi/*.csproj ./CartingService.WebApi/
COPY CartingService.BLL/*.csproj ./CartingService.BLL/
COPY CartingService.DAL/*.csproj ./CartingService.DAL/
COPY CartingService.Domain/*.csproj ./CartingService.Domain/
COPY CartingService.UnitTests/*.csproj ./CartingService.UnitTests/

# Restore dependencies
RUN dotnet restore

# Copy the remaining source code and publish
COPY . ./
RUN dotnet publish CartingService.WebApi/CartingService.WebApi.csproj -c Release -o /app/out

# Stage 2: Minimal runtime environment
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build-env /app/out .

USER app
EXPOSE 8080

ENTRYPOINT ["dotnet", "CartingService.WebApi.dll"]