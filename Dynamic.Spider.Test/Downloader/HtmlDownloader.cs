using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Dynamic.Spider.Downloader
{
    public class HtmlDownloader : IDownloader
    {
        public HtmlDownloader(string baseUrl)
        {
            this.baseUrl = new Uri(baseUrl);
        }
        private static readonly HttpClient httpClient = new HttpClient();
        private readonly Uri baseUrl;

        public Task<string> Down(string url)
        {
            var targetUrl = new Uri(baseUrl, url);
            return httpClient.GetStringAsync(targetUrl);
        }
    }
}
