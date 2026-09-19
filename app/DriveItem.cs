using Wpf.Ui.Controls;

namespace BitLockerLock;

public sealed record DriveItem(string Letter, string Title, string Status, bool IsLocked, bool IsSystem)
{
    public bool CanLock => !IsLocked && !IsSystem;
    public bool ShowButton => !IsLocked;
    public SymbolRegular Icon => IsLocked ? SymbolRegular.LockClosed24 : SymbolRegular.LockOpen24;
}
