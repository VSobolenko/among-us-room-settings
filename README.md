<div align="center">

# Among Us Room Settings

_Веб-конфигуратор для генерации `normalHostOptions` строки для **Among Us**._

[![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-Web_API-5C2D91?logo=dotnet&logoColor=white)](https://learn.microsoft.com/aspnet/core)
[![JavaScript](https://img.shields.io/badge/Frontend-HTML%2FCSS%2FJS-F7DF1E?logo=javascript&logoColor=black)](https://developer.mozilla.org/docs/Web/JavaScript)
[![Render](https://img.shields.io/badge/Deploy-Render-46E3B7?logo=render&logoColor=black)](https://render.com/)

🌐 Demo: [among-us-room-settings.onrender.com](https://among-us-room-settings.onrender.com/index.html)

</div>

## Description

Этот проект превращает набор игровых настроек **Among Us** в готовую Base64-строку для поля `normalHostOptions`.

Внутри проекта есть:
- веб-интерфейс для ручной настройки параметров комнаты;
- готовые пресеты (`Default`, `Gexetr`, `Пупсик & Шпекси`, `LaF1`, `Clown`, `Nova`, `Ну мы`);
- серверный endpoint для сериализации настроек;
- простая защита доступа через ежедневный ключ.

## Features

- Генерация строки `normalHostOptions`
- Настройка основных игровых параметров
- Настройка ролей и их вероятностей
- Предустановленные наборы настроек
- Доступ через ежедневный ключ
- Статический frontend + ASP.NET Core backend

## How to Run

### Local run

1. Клонируйте репозиторий.
2. Убедитесь, что установлен **.NET 8 SDK**.
3. Запустите приложение:

```powershell
dotnet run --project .\AmongUsRoomSettings\AmongUsRoomSettings.csproj
```

После запуска откройте браузер по одному из локальных адресов из `launchSettings`:
- `http://localhost:5008`
- `https://localhost:7071`

В режиме разработки также доступен Swagger:
- `https://localhost:7071/swagger`

### Docker

```powershell
docker build -t among-us-room-settings .
docker run --rm -p 8080:8080 among-us-room-settings
```

> Если будете запускать контейнер локально, при необходимости передайте `ASPNETCORE_URLS`, чтобы явно задать порт прослушивания.

## How to Use

1. Откройте [веб-конфигуратор](https://among-us-room-settings.onrender.com/index.html).
2. Введите ежедневный ключ доступа.
3. Выберите один из пресетов или настройте параметры вручную.
4. Сгенерируйте строку `normalHostOptions`.
5. Откройте файл `settings.amogus` на устройстве.
6. Найдите поле `normalHostOptions` и замените его значение на сгенерированную строку.
7. Сохраните файл и запустите игру.

> Важно: во время редактирования файла игра должна быть полностью закрыта.

## Access Key Logic

Доступ к интерфейсу открывается через ключ, который зависит от:
- дня недели;
- чётности номера недели;
- московского времени (`UTC+3`).

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

Пример: если сегодня **Tuesday** и неделя чётная, ключ будет **1957**.

## API

### `POST /api/access/check`

Проверяет ключ доступа.

### `POST /encode`

Принимает JSON-модель игровых настроек и возвращает готовую Base64-строку.

Frontend уже использует этот endpoint, поэтому руками вызывать его обычно не нужно.

## Paths to `settings.amogus`

### Android

```text
\Phone\Android\data\com.innersloth.spacemafia\files\
```

Полезные приложения:
- [Marc File Manager](https://play.google.com/store/apps/details?id=com.marc.files&hl=ru)
- [QuickEdit](https://play.google.com/store/apps/details?id=com.rhmsoft.edit)

### Windows

```text
%userprofile%\AppData\LocalLow\Innersloth\Among Us\settings.amogus
```

### iPhone / iOS

Недоступно из-за ограничений доступа к файлам приложения.

## Tech Stack

- **Backend:** ASP.NET Core Web API (.NET 8)
- **Frontend:** HTML, CSS, JavaScript
- **Serialization:** кастомная сериализация настроек Among Us в Base64
- **Docs / debug:** Swagger в Development-режиме
- **Hosting:** Render
- **Tests:** NUnit

## Project Structure

```text
AmongUsRoomSettings/
├─ AmongUsRoomSettings/               # ASP.NET Core приложение
│  ├─ AmongUs/                        # модели и сериализация игровых настроек
│  ├─ Controllers/                    # API контроллеры
│  ├─ Utils/                          # вспомогательные утилиты
│  └─ wwwroot/                        # frontend
├─ AmongUsRoomSettings.UnitTests/     # unit-тесты
└─ Dockerfile
```

## Notes

- Проект ориентирован на генерацию строки настроек, а не на модификацию игровых файлов напрямую.
- Значение `normalHostOptions` записывается пользователем вручную в `settings.amogus`.
- Проект не аффилирован с **Innersloth** и создан как фанатский utility tool.

<div align="center">
  <sub>Made with ❤️ for custom Among Us lobbies</sub>
</div>
