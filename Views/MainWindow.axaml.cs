using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using KartURL.Common;
using KartURL.ViewModels;

namespace KartURL.Views
{
    public class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            DataContext = new MainWindowViewModel(new ExecutableFileDialog(this));
            Title = $"KartURL {GetType().Assembly.GetName().Version} - Ａ Ｅ Ｓ Ｔ Ｈ Ｅ Ｔ Ｉ Ｃ edition";
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
