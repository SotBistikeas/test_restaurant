FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["src/FoodCost.Web.Host/FoodCost.Web.Host.csproj", "src/FoodCost.Web.Host/"]
COPY ["src/FoodCost.Web.Core/FoodCost.Web.Core.csproj", "src/FoodCost.Web.Core/"]
COPY ["src/FoodCost.EntityFrameworkCore/FoodCost.EntityFrameworkCore.csproj", "src/FoodCost.EntityFrameworkCore/"]
COPY ["src/FoodCost.Core/FoodCost.Core.csproj", "src/FoodCost.Core/"]
COPY ["src/FoodCost.Application/FoodCost.Application.csproj", "src/FoodCost.Application/"]
RUN dotnet restore "src/FoodCost.Web.Host/FoodCost.Web.Host.csproj"
COPY . .
WORKDIR "/src/src/FoodCost.Web.Host"
RUN dotnet build "FoodCost.Web.Host.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "FoodCost.Web.Host.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .



ENTRYPOINT ["dotnet", "FoodCost.Web.Host.dll"]