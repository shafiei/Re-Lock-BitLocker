using System.Windows;
using Wpf.Ui.Appearance;
using Wpf.Ui.Controls;

namespace BitLockerLock;

public partial class MainWindow : FluentWindow
{
    private bool _busy;

    public MainWindow()
    {
        SystemThemeWatcher.Watch(this);   // follow Windows light/dark mode live
        InitializeComponent();

        FlowDirection = Strings.IsFa ? FlowDirection.RightToLeft : FlowDirection.LeftToRight;

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
            DriveList.ItemsSource = drives;
            EmptyState.Visibility = drives.Count == 0 ? Visibility.Visible : Visibility.Collapsed;
            LockAllButton.IsEnabled = drives.Any(d => d.CanLock);
        }
        catch
        {
            StatusText.Text = Strings.ReadFailed;
        }
        finally
        {
            _busy = false;
        }
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
        int done = 0;
        string? failure = null;

        try
        {
            foreach (var item in items)
            {
                var result = await Task.Run(() => BitLockerService.Lock(item.Letter));
                if (result.Ok) done++;
                else failure = string.Format(Strings.LockFailed, $"{item.Letter}:", $"0x{result.Code:X8}");
            }
        }
        catch
        {
            failure = Strings.ReadFailed;
        }
        finally
        {
            _busy = false;
        }

        StatusText.Text = failure ??
            (items.Count == 1
                ? string.Format(Strings.LockedOk, $"{items[0].Letter}:")
                : string.Format(Strings.LockedAllOk, done));

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
