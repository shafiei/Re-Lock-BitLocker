# Re-Lock BitLocker

Re-lock a BitLocker drive after you've unlocked it, without restarting Windows.

There are two ways to use it:

| | For | Get it |
|---|---|---|
| **App** (recommended) | Everyday use. Windows 11 look, one-click lock. | `ReLockBitLocker-Setup.exe` from [Releases](https://github.com/shafiei/Re-Lock-BitLocker/releases) |
| **Script** | Quick and portable, nothing to install. | [`Re-Lock-BitLocker.bat`](Re-Lock-BitLocker.bat) |

## App

- Lists your BitLocker drives automatically, with their status
- One click to lock a drive, or **Lock all**
- Asks before locking, because open files on the drive are closed
- Follows the Windows light/dark theme
- Persian (RTL) or English, depending on the Windows display language
- Self-contained: nothing else to install

## Script

1. Download `Re-Lock-BitLocker.bat`.
2. Double-click it and approve the administrator prompt.
3. Type the number of the drive to lock, `A` to lock all, or `Q` to quit.

## Notes

- Administrator rights are required (both the app and the script ask for them).
- The Windows drive (usually `C:`) can't be locked; BitLocker doesn't allow it.
- Locking uses *force dismount*, so anything still open on that drive is closed. Save your work first.
- Windows 10/11, 64-bit. BitLocker must be available on your edition of Windows.

## Build from source

You need the .NET 8 SDK, and [Inno Setup 6](https://jrsoftware.org/isinfo.php) for the installer.

```
build.bat
```

This publishes the app to `app\publish` and, if Inno Setup is installed, builds `installer\output\ReLockBitLocker-Setup.exe`.

To publish a release automatically, push a tag such as `v1.0.0`; the workflow in `.github/workflows/release.yml` builds the installer and attaches it to the release.

## فارسی

برنامه‌ای کوچک برای قفل کردن دوباره‌ی درایو BitLocker بعد از بازکردنش، بدون ری‌استارت ویندوز.

- **برنامه (پیشنهادی):** `ReLockBitLocker-Setup.exe` را از بخش Releases دانلود و نصب کنید.
- **اسکریپت:** فایل `Re-Lock-BitLocker.bat` را اجرا کنید (بدون نیاز به نصب).

هر دو نیاز به دسترسی ادمین دارند. درایو ویندوز (معمولاً `C:`) قابل قفل شدن نیست، و فایل‌های بازِ درایو هنگام قفل بسته می‌شوند.

## License

MIT
