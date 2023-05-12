FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/FretWeb/FretWeb.csproj", "src/FretWeb/"]
COPY ["src/Music/Music.csproj", "src/Music/"]
COPY ["src/Fretboards/Fretboards.csproj", "src/Fretboards/"]
RUN dotnet restore "src/FretWeb/FretWeb.csproj"
COPY . .
WORKDIR "/src/src/FretWeb"
RUN dotnet build "FretWeb.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FretWeb.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FretWeb.dll"]
