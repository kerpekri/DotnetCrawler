using HtmlAgilityPack;
using System.Threading.Tasks;

namespace DotnetCrawler.Downloader.Implementations
{
    public class DotnetCrawlerDownloader : IDotnetCrawlerDownloader
    {
        private readonly IWebClientService _webClientService;

        public DotnetCrawlerDownloader(IWebClientService webClientService)
        {
            _webClientService = webClientService;
        }

        public async Task<HtmlDocument> Download(string url)
        {
            return await _webClientService.FromWebAsync(url);
        }
    }
}
