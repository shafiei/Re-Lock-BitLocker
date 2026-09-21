using System.Windows;

namespace BitLockerLock;

public partial class App : Application
{
    public App()
    {
        // Last line of defence: show a readable message instead of Windows' crash dialog.
        DispatcherUnhandledException += (_, e) =>
        {
            MessageBox.Show(e.Exception.Message, Strings.AppTitle, MessageBoxButton.OK, MessageBoxImage.Error);
            e.Handled = true;
        };
    }
}
