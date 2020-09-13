using System.Threading.Tasks;

namespace KartURL.Common
{
    public interface IFileDialog
    {
        Task<string> ShowAsync();
    }
}