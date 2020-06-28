using DotnetCrawler.Request;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotnetCrawler.Downloader
{
    public interface IDotnetCrawlerPageLinkReader
    {
        Task<IEnumerable<string>> GetLinksAsync(DotnetCrawlerRequest request, int level = 0);
    }
}
