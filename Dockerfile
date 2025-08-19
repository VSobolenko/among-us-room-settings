# --- Build stage ---
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Копируем csproj и восстанавливаем зависимости
COPY AmongUsRoomSettings/AmongUsRoomSettings.csproj ./
RUN dotnet restore

# Копируем весь проект и публикуем релиз
COPY AmongUsRoomSettings/. ./
RUN dotnet publish -c Release -o out

# --- Runtime stage ---
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out ./

ENTRYPOINT ["dotnet", "AmongUsRoomSettings.dll"]
