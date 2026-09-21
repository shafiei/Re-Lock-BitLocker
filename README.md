<p align="center">
  <img src="docs/logo.png" alt="Re-Lock BitLocker logo" width="160">
</p>

<h1 align="center">Re-Lock BitLocker</h1>

<p align="center">
  Lock your BitLocker drives again in one click, without restarting Windows.
</p>

<p align="center">
  <a href="https://github.com/shafiei/Re-Lock-BitLocker/releases/latest"><img alt="Latest release" src="https://img.shields.io/github/v/release/shafiei/Re-Lock-BitLocker?color=0b5fd6"></a>
  <a href="LICENSE"><img alt="MIT license" src="https://img.shields.io/github/license/shafiei/Re-Lock-BitLocker?color=0b5fd6"></a>
  <img alt="Windows 10 and 11" src="https://img.shields.io/badge/Windows-10%20%7C%2011-0b5fd6">
  <a href="https://github.com/shafiei/Re-Lock-BitLocker/releases"><img alt="Downloads" src="https://img.shields.io/github/downloads/shafiei/Re-Lock-BitLocker/total?color=0b5fd6"></a>
</p>

---

Windows keeps a BitLocker drive open after you unlock it, until you restart. **Re-Lock BitLocker** locks it again right away, so you can unplug it or walk away without rebooting.

There are two ways to use it:

| | Best for | Get it |
|---|---|---|
| **App** (recommended) | Everyday use. Windows 11 look, one-click lock. | [**Download the installer**](https://github.com/shafiei/Re-Lock-BitLocker/releases/latest) |
| **Script** | Quick and portable. Nothing to install. | [`Re-Lock-BitLocker.bat`](Re-Lock-BitLocker.bat) |

## App

1. Download `ReLockBitLocker-Setup.exe` from the [latest release](https://github.com/shafiei/Re-Lock-BitLocker/releases/latest).
2. Run it and follow the steps. Nothing else needs to be installed.
3. Open **Re-Lock BitLocker** from the Start menu and press **Lock** next to a drive.

What it does:

- Lists your BitLocker drives automatically, with their current state
- Locks one drive with a click, or all of them with **Lock all**
- Asks before locking, because files still open on the drive are closed
- Matches Windows 11 (Mica, rounded corners) and follows the light/dark theme
- Shows the interface in Persian (right-to-left) or English, following your Windows display language

## Script

1. Download `Re-Lock-BitLocker.bat`.
2. Double-click it and approve the administrator prompt.
3. Type the number of the drive to lock, `A` to lock all, or `Q` to quit.

## Good to know

- Administrator rights are required. Both the app and the script ask for them.
- The Windows drive (usually `C:`) can't be locked. BitLocker doesn't allow it.
- Locking uses *force dismount*: anything still open on that drive is closed. Save your work first.
- Windows 10 or 11, 64-bit, on an edition that includes BitLocker (Pro, Enterprise, Education).
- Windows may show a "Windows protected your PC" warning because the installer isn't code-signed. Choose **More info**, then **Run anyway**. If you'd like to check the download first, compare its hash with `SHA256SUMS.txt` from the release:

  ```
  certutil -hashfile ReLockBitLocker-Setup.exe SHA256
  ```

- To uninstall, use **Settings > Apps > Installed apps**.

## Build from source

You need the [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0), and [Inno Setup 6](https://jrsoftware.org/isinfo.php) if you want the installer.

```
build.bat
```

This publishes a self-contained `ReLockBitLocker.exe` to `app\publish` and, if Inno Setup is installed, builds `installer\output\ReLockBitLocker-Setup.exe`.

To publish a release, push a version tag such as `v1.0.1`. The workflow in `.github/workflows/release.yml` builds the installer and attaches it to a GitHub Release, together with a checksum file.

```
app/          WPF app (C#, .NET 8, WPF-UI)
installer/    Inno Setup script and wizard images
docs/         Logo and images used by this page
```

## فارسی

Re-Lock BitLocker درایوهای BitLocker را بدون ری‌استارت ویندوز دوباره قفل می‌کند.

- **برنامه (پیشنهادی):** فایل `ReLockBitLocker-Setup.exe` را از [صفحه‌ی Releases](https://github.com/shafiei/Re-Lock-BitLocker/releases/latest) دانلود و نصب کنید، سپس کنار درایو موردنظر روی **قفل کن** بزنید.
- **اسکریپت:** فایل `Re-Lock-BitLocker.bat` را اجرا کنید (بدون نیاز به نصب).

هر دو نیاز به دسترسی ادمین دارند. درایو ویندوز (معمولاً `C:`) قابل قفل شدن نیست، و فایل‌های بازِ درایو هنگام قفل بسته می‌شوند، پس قبل از قفل کارتان را ذخیره کنید. اگر زبان ویندوز فارسی باشد، برنامه هم فارسی و راست‌به‌چپ نمایش داده می‌شود.

## License

[MIT](LICENSE)
