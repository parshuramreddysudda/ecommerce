FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ECommerce.Api.Customers/ECommerce.Api.Customers.csproj", "ECommerce.Api.Customers/"]
RUN dotnet restore "ECommerce.Api.Customers/ECommerce.Api.Customers.csproj"
COPY . .
WORKDIR "/src/ECommerce.Api.Customers"
RUN dotnet build "ECommerce.Api.Customers.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ECommerce.Api.Customers.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ECommerce.Api.Customers.dll"]
