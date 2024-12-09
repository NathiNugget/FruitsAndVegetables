FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app

EXPOSE 8081
EXPOSE 8080


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Debug
WORKDIR /src
COPY ["FruitREST/FruitREST.csproj", "FruitREST/"]
RUN dotnet restore "./FruitREST/FruitREST.csproj"
COPY . .



# Install libgdiplus and other dependencies for image processing support
RUN apt-get update && \
    apt-get install -y libgdiplus libc6-dev fontconfig

WORKDIR "/src/FruitREST"
RUN dotnet build "./FruitREST.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Debug
RUN dotnet publish "./FruitREST.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FruitREST.dll"]
