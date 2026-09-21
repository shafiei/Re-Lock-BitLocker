using System.Management;
using System.Reflection;
using System.Windows;
using Wpf.Ui.Appearance;
using Wpf.Ui.Controls;

namespace BitLockerLock;

public partial class MainWindow : FluentWindow
{
    private bool _busy;
    private bool _statusIsError;

    public MainWindow()
    {
        SystemThemeWatcher.Watch(this);   // follow Windows light/dark mode live
        InitializeComponent();

        FlowDirection = Strings.IsFa ? FlowDirection.RightToLeft : FlowDirection.LeftToRight;

        var version = Assembly.GetExecutingAssembly().GetName().Version;
        VersionText.Text = version is null ? string.Empty : $"v{version.Major}.{version.Minor}.{version.Build}";

        // Also fires on startup and whenever the user comes back to the window.
        Activated += async (_, _) => await RefreshAsync();
    }

    private async Task RefreshAsync()
    {
        if (_busy) return;
        _busy = true;
        try
        {
            var drives = await Task.Run(BitLockerService.GetDrives);
            ShowDrives(drives);
        }
        catch (ManagementException ex) when (ex.ErrorCode is ManagementStatus.InvalidNamespace or ManagementStatus.InvalidClass)
        {
            ShowError(Strings.NotAvailable);
        }
        catch
        {
            ShowError(Strings.ReadFailed);
        }
        finally
        {
            _busy = false;
        }
    }

    private void ShowDrives(IReadOnlyList<DriveItem> drives)
    {
        DriveList.ItemsSource = drives;
        EmptyState.Visibility = drives.Count == 0 ? Visibility.Visible : Visibility.Collapsed;
        LockAllButton.IsEnabled = drives.Any(d => d.CanLock);

        if (_statusIsError)
        {
            StatusText.Text = string.Empty;
            _statusIsError = false;
        }
    }

    private void ShowError(string message)
    {
        DriveList.ItemsSource = null;
        EmptyState.Visibility = Visibility.Visible;
        LockAllButton.IsEnabled = false;
        StatusText.Text = message;
        _statusIsError = true;
    }

    private async void RefreshButton_Click(object sender, RoutedEventArgs e) => await RefreshAsync();

    private async void LockButton_Click(object sender, RoutedEventArgs e)
    {
        if ((sender as FrameworkElement)?.DataContext is not DriveItem item) return;

        bool ok = await ConfirmAsync(
            string.Format(Strings.ConfirmTitle, $"{item.Letter}:"),
            Strings.ConfirmBody);
        if (ok) await LockAsync(new[] { item });
    }

    private async void LockAllButton_Click(object sender, RoutedEventArgs e)
    {
        var targets = (DriveList.ItemsSource as IEnumerable<DriveItem>)?.Where(d => d.CanLock).ToList();
        if (targets is null || targets.Count == 0) return;

        bool ok = await ConfirmAsync(
            string.Format(Strings.ConfirmAllTitle, targets.Count),
            Strings.ConfirmAllBody);
        if (ok) await LockAsync(targets);
    }

    private async Task LockAsync(IReadOnlyList<DriveItem> items)
    {
        _busy = true;
        DriveList.IsEnabled = false;
        RefreshButton.IsEnabled = false;
        LockAllButton.IsEnabled = false;

        int done = 0;
        var failed = new List<(string Letter, uint Code)>();

        try
        {
            foreach (var item in items)
            {
                LockResult result;
                try
                {
                    result = await Task.Run(() => BitLockerService.Lock(item.Letter));
                }
                catch
                {
                    result = new LockResult(false, 0xFFFFFFFF);
                }

                if (result.Ok) done++;
                else failed.Add((item.Letter, result.Code));
            }
        }
        finally
        {
            _busy = false;
            DriveList.IsEnabled = true;
            RefreshButton.IsEnabled = true;
        }

        StatusText.Text = failed.Count switch
        {
            0 when items.Count == 1 => string.Format(Strings.LockedOk, $"{items[0].Letter}:"),
            0 => string.Format(Strings.LockedAllOk, done),
            1 when done == 0 => string.Format(Strings.LockFailed, $"{failed[0].Letter}:", $"0x{failed[0].Code:X8}"),
            _ => string.Format(Strings.SomeFailed, done, failed.Count),
        };
        _statusIsError = false;

        await RefreshAsync();
    }

    private async Task<bool> ConfirmAsync(string title, string body)
    {
        var box = new Wpf.Ui.Controls.MessageBox
        {
            Title = title,
            Content = body,
            PrimaryButtonText = Strings.Lock,
            CloseButtonText = Strings.Cancel,
            FlowDirection = FlowDirection,
        };

        var result = await box.ShowDialogAsync();
        return result == Wpf.Ui.Controls.MessageBoxResult.Primary;
    }
}
