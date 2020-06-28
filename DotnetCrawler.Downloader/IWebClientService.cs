using HtmlAgilityPack;
using System.Threading.Tasks;

namespace DotnetCrawler.Downloader
{
    public interface IWebClientService
    {
        Task<HtmlDocument> FromWebAsync(string url);
    }
}
