using System.Globalization;

namespace BitLockerLock;

/// <summary>UI text. Persian when the Windows UI language is Persian, English otherwise.</summary>
public static class Strings
{
    public static readonly bool IsFa =
        CultureInfo.CurrentUICulture.TwoLetterISOLanguageName == "fa";

    private static string T(string en, string fa) => IsFa ? fa : en;

    public static string AppTitle => "Re-Lock BitLocker";
    public static string Heading => T("Lock a drive", "قفل کردن درایو");
    public static string Subtitle => T("Choose a drive to lock it again.", "درایوی را که می‌خواهید دوباره قفل شود انتخاب کنید.");
    public static string Refresh => T("Refresh", "بازخوانی");
    public static string LockAll => T("Lock all", "قفل همه");
    public static string Lock => T("Lock", "قفل کن");
    public static string Cancel => T("Cancel", "انصراف");

    public static string Locked => T("Locked", "قفل است");
    public static string Unlocked => T("Unlocked", "باز است");
    public static string Encrypting => T("Unlocked, encrypting {0}%", "باز است، در حال رمزگذاری {0}٪");
    public static string SystemDrive => T("Windows drive, can't be locked", "درایو ویندوز، قابل قفل شدن نیست");
    public static string NoLabel => T("Local Disk", "دیسک محلی");

    public static string EmptyTitle => T("No BitLocker drives found", "درایو BitLocker پیدا نشد");
    public static string EmptyHint => T("Connect an encrypted drive, then select Refresh.", "یک درایو رمزگذاری‌شده وصل کنید و بازخوانی را بزنید.");

    public static string ConfirmTitle => T("Lock {0}?", "درایو {0} قفل شود؟");
    public static string ConfirmAllTitle => T("Lock {0} drives?", "{0} درایو قفل شوند؟");
    public static string ConfirmBody => T("Files that are still open on this drive will be closed. Save your work first.",
                                          "فایل‌های بازِ این درایو بسته می‌شوند. ابتدا کارتان را ذخیره کنید.");
    public static string ConfirmAllBody => T("Files that are still open on these drives will be closed. Save your work first.",
                                             "فایل‌های بازِ این درایوها بسته می‌شوند. ابتدا کارتان را ذخیره کنید.");

    public static string LockedOk => T("{0} locked.", "{0} قفل شد.");
    public static string LockedAllOk => T("{0} drives locked.", "{0} درایو قفل شد.");
    public static string LockFailed => T("Couldn't lock {0} (code {1}). Close anything using it and try again.",
                                         "قفل کردن {0} انجام نشد (کد {1}). برنامه‌هایی را که از آن استفاده می‌کنند ببندید و دوباره تلاش کنید.");
    public static string SomeFailed => T("{0} locked, {1} couldn't be locked. Close anything using them and try again.",
                                         "{0} درایو قفل شد، {1} درایو قفل نشد. برنامه‌هایی را که از آن‌ها استفاده می‌کنند ببندید و دوباره تلاش کنید.");

    public static string ReadFailed => T("Couldn't read the drives. Make sure the app is running as administrator.",
                                         "خواندن درایوها ممکن نشد. برنامه باید با دسترسی ادمین اجرا شود.");
    public static string NotAvailable => T("BitLocker isn't available on this edition of Windows.",
                                           "BitLocker در این نسخه از ویندوز در دسترس نیست.");
}
