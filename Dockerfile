﻿FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["FretWeb.sln", "."]
COPY ["src/FretWeb/FretWeb.csproj", "src/FretWeb/"]
COPY ["src/Music/Music.csproj", "src/Music/"]
COPY ["src/Fretboards/Fretboards.csproj", "src/Fretboards/"]
COPY ["test/Fretboards.Tests/Fretboards.Tests.csproj", "test/Fretboards.Tests/"]
COPY ["test/FretWeb.SmokeTest/FretWeb.SmokeTest.csproj", "test/FretWeb.SmokeTest/"]
COPY ["test/Music.Tests/Music.Tests.csproj", "test/Music.Tests/"]
RUN dotnet restore
COPY . .
RUN dotnet build --no-restore -c Release -o /app/build
RUN dotnet test -c Release -o /app/build --no-build

FROM build AS publish
RUN dotnet publish "src/FretWeb/FretWeb.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FretWeb.dll"]
