# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS base
WORKDIR /app
EXPOSE 80


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/TaskAGSR/TaskAGSR.csproj", "src/TaskAGSR/"]
COPY ["src/System.Runtime.CompilerServices/System.Runtime.CompilerServices.csproj", "src/System.Runtime.CompilerServices/"]
COPY ["src/TaskAGSR.Infrastructure/TaskAGSR.Infrastructure.csproj", "src/TaskAGSR.Infrastructure/"]
COPY ["src/TaskAGSR.Application/TaskAGSR.Application.csproj", "src/TaskAGSR.Application/"]
COPY ["src/TaskAGSR.Domain/TaskAGSR.Domain.csproj", "src/TaskAGSR.Domain/"]
RUN dotnet restore "./src/TaskAGSR/TaskAGSR.csproj"
COPY . .
WORKDIR "/src/src/TaskAGSR"
RUN dotnet build "./TaskAGSR.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./TaskAGSR.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TaskAGSR.dll"]