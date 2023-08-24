# docker run --name SteamTradeBot -d -it -p 5050:80 -p 5060:443 -e SELENIUM_HOST="http://host.docker.internal:5051" -e POSTGRES_CONNECTION_STRING="Host=host.docker.internal;Port=32768;Database=MarketDatabase;Username=postgres;Password=postgrespw" steam-trade-bot
# docker build -t unh0ly/steam-trade-bot:latest .
# docker push unh0ly/steam-trade-bot

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "SteamTradeBot.Backend.csproj"
WORKDIR "/src"
RUN dotnet build "SteamTradeBot.Backend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SteamTradeBot.Backend.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SteamTradeBot.Backend.dll"]