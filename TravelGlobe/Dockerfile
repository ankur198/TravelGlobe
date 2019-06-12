FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["TravelGlobe/TravelGlobe.csproj", "TravelGlobe/"]
RUN dotnet restore "TravelGlobe/TravelGlobe.csproj"
COPY . .
WORKDIR "/src/TravelGlobe"
RUN dotnet build "TravelGlobe.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "TravelGlobe.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "TravelGlobe.dll"]
