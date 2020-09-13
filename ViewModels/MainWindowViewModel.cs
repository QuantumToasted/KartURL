using System.Collections.ObjectModel;
using System.Reactive;
using System.Runtime.InteropServices;
using KartURL.Common;
using ManagedBass;
using ReactiveUI;

namespace KartURL.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private static readonly MediaPlayer MediaPlayer = new MediaPlayer {Volume = 0, Loop = true};
        private static bool _isPlaying;
        private readonly object _lock = new object();
        private readonly IFileDialog _dialog;
        private string _kartExecutablePath;
        private string _commandLineArguments;
        private ObservableCollection<string> _platforms;
        private string _platform;
        private string _resultMessage;

        public MainWindowViewModel(IFileDialog dialog)
        {
            lock (_lock)
            {
                if (_isPlaying) return;
                _isPlaying = true;
                MediaPlayer.LoadAsync("abstract.ogg");
                MediaPlayer.MediaLoaded += _ => MediaPlayer.Play();
            }

            _dialog = dialog;
            _kartExecutablePath = string.Empty;
            _commandLineArguments = string.Empty;
            _platforms = new ObservableCollection<string> {"Windows", "OSX", "Linux"};

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                _platform = _platforms[0];
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                _platform = _platforms[1];
            }
            else
            {
                _platform = _platforms[2];
            }

            _resultMessage = string.Empty;
        }

        public ReactiveCommand<Unit, Unit> Browse => ReactiveCommand.CreateFromTask(async () =>
        {
            KartExecutablePath = await _dialog.ShowAsync();
        });

        public ReactiveCommand<Unit, Unit> ToggleMute => ReactiveCommand.Create(() =>
        {
            MediaPlayer.Volume = MediaPlayer.Volume < 0.125d ? 0.25d : 0.0d;
        });

        public string KartExecutablePath
        {
            get => _kartExecutablePath;
            set => this.RaiseAndSetIfChanged(ref _kartExecutablePath, value);
        }

        public string CommandLineArguments
        {
            get => _commandLineArguments;
            set => this.RaiseAndSetIfChanged(ref _commandLineArguments, value);
        }

        public ObservableCollection<string> Platforms
        {
            get => _platforms;
            set => this.RaiseAndSetIfChanged(ref _platforms, value);
        }

        public string Platform
        {
            get => _platform;
            set => this.RaiseAndSetIfChanged(ref _platform, value);
        }

        public string ResultMessage
        {
            get => _resultMessage;
            set => this.RaiseAndSetIfChanged(ref _resultMessage, value);
        }
    }
}
