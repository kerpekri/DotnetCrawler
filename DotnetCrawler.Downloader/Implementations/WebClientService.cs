using HtmlAgilityPack;
using System.Threading.Tasks;

namespace DotnetCrawler.Downloader.Implementations
{
    public class WebClientService : IWebClientService
    {
        public async Task<HtmlDocument> FromWebAsync(string url)
        {
            HtmlWeb htmlWeb = new HtmlWeb();
            return await htmlWeb.LoadFromWebAsync(url);
        }
    }
}
