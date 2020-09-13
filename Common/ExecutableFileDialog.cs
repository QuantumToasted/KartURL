using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Avalonia.Controls;

namespace KartURL.Common
{
    public sealed class ExecutableFileDialog : IFileDialog
    {
        private readonly Window _window;

        public ExecutableFileDialog(Window window)
        {
            _window = window;
        }

        public async Task<string> ShowAsync()
        {
            var dialog = new OpenFileDialog
            {
                AllowMultiple = false,
                Title = "SRB2Kart executable location?"
            };

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                dialog.InitialFileName = "srb2kart.exe";
                dialog.Filters.Add(new FileDialogFilter
                {
                    Extensions = new List<string>{"exe"},
                    Name = "Executable files"
                });
            }
            else
            {
                dialog.InitialFileName = "srb2kart";
            }

            return string.Join(' ', await dialog.ShowAsync(_window));
        }
    }
}