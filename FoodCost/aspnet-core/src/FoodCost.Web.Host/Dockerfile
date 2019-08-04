FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS base
WORKDIR /app
EXPOSE 21021
EXPOSE 44316

FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /src
COPY src/FoodCost.Web.Host/FoodCost.Web.Host.csproj src/FoodCost.Web.Host/
RUN dotnet restore src/FoodCost.Web.Host/FoodCost.Web.Host.csproj
COPY . .
WORKDIR /src/src/FoodCost.Web.Host
RUN dotnet build FoodCost.Web.Host.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish FoodCost.Web.Host.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "FoodCost.Web.Host.dll"]