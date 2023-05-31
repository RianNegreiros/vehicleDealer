FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["vehicleDealer.csproj", "."]
RUN dotnet restore "./vehicleDealer.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "vehicleDealer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "vehicleDealer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "vehicleDealer.dll"]
