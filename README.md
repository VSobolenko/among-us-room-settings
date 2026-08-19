<div align="center">

# Among Us Room Settings

_Web configurator for generating a `normalHostOptions` string for **Among Us**._

[![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-Web_API-5C2D91?logo=dotnet&logoColor=white)](https://learn.microsoft.com/aspnet/core)
[![JavaScript](https://img.shields.io/badge/Frontend-HTML%2FCSS%2FJS-F7DF1E?logo=javascript&logoColor=black)](https://developer.mozilla.org/docs/Web/JavaScript)
[![Render](https://img.shields.io/badge/Deploy-Render-46E3B7?logo=render&logoColor=black)](https://render.com/)

🌐 Demo: [among-us-room-settings.onrender.com](https://among-us-room-settings.onrender.com/index.html)

</div>

## Description

This project turns a set of **Among Us** game settings into a ready-to-use Base64 string for the `normalHostOptions` field.

This project includes:
- a web interface for manually editing room settings;
- built-in presets (`Default`, `Gexetr`, `Пупсик & Шпекси`, `LaF1`, `Clown`, `Nova`, `Ну мы`);
- a backend endpoint for serializing settings;
- simple access protection based on a daily key.

## Features

- Generate a `normalHostOptions` string
- Configure main game parameters
- Configure roles and their probabilities
- Built-in presets
- Access via a daily key
- Static frontend + ASP.NET Core backend

## How to Run

### Local run

1. Clone the repository.
2. Make sure **.NET 8 SDK** is installed.
3. Run the application:

```powershell
dotnet run --project .\AmongUsRoomSettings\AmongUsRoomSettings.csproj
```

After startup, open your browser using one of the local URLs from `launchSettings`:
- `http://localhost:5008`
- `https://localhost:7071`

Swagger is also available in development mode:
- `https://localhost:7071/swagger`

### Docker

```powershell
docker build -t among-us-room-settings .
docker run --rm -p 8080:8080 among-us-room-settings
```

> If you run the container locally, pass `ASPNETCORE_URLS` if you need to explicitly define the listening port.

## How to Use

1. Open the [web configurator](https://among-us-room-settings.onrender.com/index.html).
2. Enter the daily access key.
3. Choose one of the presets or configure the settings manually.
4. Generate the `normalHostOptions` string.
5. Open the `settings.amogus` file on your device.
6. Find the `normalHostOptions` field and replace its value with the generated string.
7. Save the file and launch the game.

> Important: the game must be fully closed while editing the file.

## Access Key Logic

Access to the interface is controlled by a key that depends on:
- the day of the week;
- the parity of the week number;
- Moscow time (`UTC+3`).

### Schedule

| Day | Even Week | Odd Week |
|---|---:|---:|
| Monday | 4821 | 7364 |
| Tuesday | 1957 | 8642 |
| Wednesday | 3084 | 5271 |
| Thursday | 6712 | 4398 |
| Friday | 2549 | 9863 |
| Saturday | 8437 | 1205 |
| Sunday | 6174 | 3928 |

Example: if today is **Tuesday** and the week number is even, the key will be **1957**.

## API

### `POST /api/access/check`

Checks the access key.

### `POST /encode`

Accepts a JSON model of the game settings and returns a ready-to-use Base64 string.

The frontend already uses this endpoint, so you usually do not need to call it manually.

## Paths to `settings.amogus`

### Android

```text
\Phone\Android\data\com.innersloth.spacemafia\files\
```

Helpful apps:
- [Marc File Manager](https://play.google.com/store/apps/details?id=com.marc.files&hl=ru)
- [QuickEdit](https://play.google.com/store/apps/details?id=com.rhmsoft.edit)

### Windows

```text
%userprofile%\AppData\LocalLow\Innersloth\Among Us\settings.amogus
```

### iPhone / iOS

Not available due to app file access restrictions.

## Tech Stack

- **Backend:** ASP.NET Core Web API (.NET 8)
- **Frontend:** HTML, CSS, JavaScript
- **Serialization:** custom Among Us settings serialization to Base64
- **Docs / debug:** Swagger in Development mode
- **Hosting:** Render
- **Tests:** NUnit

## Project Structure

```text
AmongUsRoomSettings/
├─ AmongUsRoomSettings/               # ASP.NET Core application
│  ├─ AmongUs/                        # game settings models and serialization
│  ├─ Controllers/                    # API controllers
│  ├─ Utils/                          # helper utilities
│  └─ wwwroot/                        # frontend
├─ AmongUsRoomSettings.UnitTests/     # unit tests
└─ Dockerfile
```

## Notes

- The project is focused on generating a settings string, not directly modifying game files.
- The `normalHostOptions` value is inserted manually by the user into `settings.amogus`.
- This project is not affiliated with **Innersloth** and was created as a fan-made utility tool.

<div align="center">
  <sub>Made with ❤️ by Gexetr</sub>
</div>
