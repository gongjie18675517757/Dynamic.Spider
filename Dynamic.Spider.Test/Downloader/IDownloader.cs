using System.Threading.Tasks;

namespace Dynamic.Spider.Downloader
{
    public interface IDownloader
    {
        Task<string> Down(string url);
    }
}
