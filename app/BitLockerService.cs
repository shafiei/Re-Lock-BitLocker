using System.IO;
using System.Management;

namespace BitLockerLock;

public readonly record struct LockResult(bool Ok, uint Code);

/// <summary>Talks to BitLocker through WMI (Win32_EncryptableVolume). Needs administrator rights.</summary>
public static class BitLockerService
{
    private const string Scope = @"root\CIMV2\Security\MicrosoftVolumeEncryption";

    public static List<DriveItem> GetDrives()
    {
        var items = new List<DriveItem>();
        string systemLetter = char.ToUpperInvariant(Environment.SystemDirectory[0]).ToString();

        using var searcher = new ManagementObjectSearcher(Scope, "SELECT * FROM Win32_EncryptableVolume");
        foreach (ManagementObject volume in searcher.Get())
        {
            using (volume)
            {
                if (volume["DriveLetter"] is not string driveLetter || driveLetter.Length < 1)
                    continue;

                string letter = char.ToUpperInvariant(driveLetter[0]).ToString();
                bool isSystem = letter == systemLetter;
                bool isLocked = GetLockStatus(volume) == 1;

                uint conversion = 1;   // 1 = fully encrypted
                uint percent = 100;
                if (!isLocked)
                {
                    (conversion, percent) = GetConversion(volume);
                    if (conversion == 0) continue;   // not encrypted: nothing to lock
                }

                string status =
                    isSystem ? Strings.SystemDrive :
                    isLocked ? Strings.Locked :
                    conversion == 2 ? string.Format(Strings.Encrypting, percent) :
                    Strings.Unlocked;

                items.Add(new DriveItem(letter, BuildTitle(letter), status, isLocked, isSystem));
            }
        }

        return items.OrderBy(i => i.Letter).ToList();
    }

    public static LockResult Lock(string letter)
    {
        using var searcher = new ManagementObjectSearcher(Scope,
            $"SELECT * FROM Win32_EncryptableVolume WHERE DriveLetter = '{letter}:'");

        foreach (ManagementObject volume in searcher.Get())
        {
            using (volume)
            {
                var input = volume.GetMethodParameters("Lock");
                input["ForceDismount"] = true;   // same as manage-bde -lock -ForceDismount
                var output = volume.InvokeMethod("Lock", input, null);
                uint code = Convert.ToUInt32(output["ReturnValue"]);
                return new LockResult(code == 0, code);
            }
        }

        return new LockResult(false, 0xFFFFFFFF);
    }

    private static uint GetLockStatus(ManagementObject volume)
    {
        try
        {
            var output = volume.InvokeMethod("GetLockStatus", null, null);
            return Convert.ToUInt32(output["LockStatus"]);
        }
        catch
        {
            return 0;
        }
    }

    private static (uint Status, uint Percent) GetConversion(ManagementObject volume)
    {
        try
        {
            var input = volume.GetMethodParameters("GetConversionStatus");
            input["PrecisionFactor"] = (uint)0;
            var output = volume.InvokeMethod("GetConversionStatus", input, null);
            return (Convert.ToUInt32(output["ConversionStatus"]), Convert.ToUInt32(output["EncryptionPercentage"]));
        }
        catch
        {
            return (1, 100);
        }
    }

    private static string BuildTitle(string letter)
    {
        string label = Strings.NoLabel;
        try
        {
            var info = new DriveInfo(letter);
            if (info.IsReady && !string.IsNullOrWhiteSpace(info.VolumeLabel))
                label = info.VolumeLabel;
        }
        catch
        {
            // Locked drives can't be read; keep the default name.
        }
        return $"{label} ({letter}:)";
    }
}
