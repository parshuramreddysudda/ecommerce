﻿FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ECommerce.Api.Search/ECommerce.Api.Search.csproj", "ECommerce.Api.Search/"]
RUN dotnet restore "ECommerce.Api.Search/ECommerce.Api.Search.csproj"
COPY . ECommerce.Api.Search/
WORKDIR "/src/ECommerce.Api.Search"
RUN dotnet build "ECommerce.Api.Search.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ECommerce.Api.Search.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ECommerce.Api.Search.dll"]
