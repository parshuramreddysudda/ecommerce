FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ECommerce.Api.Orders/ECommerce.Api.Orders.csproj", "ECommerce.Api.Orders/"]
RUN dotnet restore "ECommerce.Api.Orders/ECommerce.Api.Orders.csproj"
COPY . .
WORKDIR "/src/ECommerce.Api.Orders"
RUN dotnet build "ECommerce.Api.Orders.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ECommerce.Api.Orders.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ECommerce.Api.Orders.dll"]
