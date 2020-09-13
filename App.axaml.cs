using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using KartURL.Common;
using KartURL.ViewModels;
using KartURL.Views;

namespace KartURL
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow();
                desktop.MainWindow.DataContext = new MainWindowViewModel(new ExecutableFileDialog(desktop.MainWindow));
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
