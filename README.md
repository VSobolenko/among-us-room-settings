# Among Us Room Settings

✨ A web tool for generating and customizing **Among Us** room settings with an easy-to-use interface.  
Friends can access the settings generator here:  
👉 [among-us-room-settings.onrender.com](https://among-us-room-settings.onrender.com/index.html)

---

## 🔑 Access Passwords

To make access fun and unique, a **daily password system** is used.  
The password depends on:
- **Day of the week**
- **Week number parity (even / odd)**

Here is the full schedule of passwords:

| Day        | Even Week | Odd Week |
|------------|-----------|----------|
| Monday     | `1234`    | `5678`   |
| Tuesday    | `2345`    | `6789`   |
| Wednesday  | `3456`    | `7890`   |
| Thursday   | `4567`    | `8901`   |
| Friday     | `5678`    | `9012`   |
| Saturday   | `6789`    | `1235`   |
| Sunday     | `7890`    | `2346`   |

✅ Example:  
If today is **Tuesday** and the current week number is even → password will be **2345**.  
If it’s odd week → password will be **6789**.

---

## 📂 Paths to Settings File

- **Android:**  
  ```
  \Phone\Android\data\com.innersloth.spacemafia\files
  ```

- **PC (Windows):**  
  ```
  %userprofile%/appdata/locallow/innersloth/Among Us/settings.amogus
  ```

- **iOS:**  
  ❌ Not available (file access restricted)

💡 On Android, you can use [this file manager app](https://play.google.com/store/apps/details?id=com.marc.files&hl=ru) to open the directory.

---

## 🚀 How to Use

1. Open [the generator](https://among-us-room-settings.onrender.com/index.html).
2. Enter the **daily password** (see the table above).
3. Generate your **normalHostOptions** string.
4. Open the `settings.amogus` file on your device.
5. Replace the value at line **35 (normalHostOptions)** with the generated string.
6. Save and enjoy custom room settings in Among Us 🎮

---

## 🛠️ Tech Stack

- Backend: **ASP.NET Core Web API**
- Frontend: **HTML, CSS, JavaScript**
- Hosting: **Render.com**
- Extra: Simple **password protection system** based on day/week parity

---

## ✨ Why

This project was created as a fun experiment to customize and share **Among Us** room settings with friends.  
It is not affiliated with Innersloth.  
