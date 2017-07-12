using System.Threading.Tasks;

namespace BooksApp.Services.DataServices
{
    public interface IBookDownloader
    {
        Task<string> DownloadStringAsync(string url);
    }
}