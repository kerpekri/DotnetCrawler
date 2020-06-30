using DotnetCrawler.Request;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DotnetCrawler.Downloader.Implementations
{
    public class DotnetCrawlerPageLinkReader : IDotnetCrawlerPageLinkReader
    {
        private readonly IWebClientService _webClientService;

        public DotnetCrawlerPageLinkReader(IWebClientService webClientService)
        {
            _webClientService = webClientService;
        }

        public async Task<IEnumerable<string>> GetLinksAsync(DotnetCrawlerRequest request, int level = 0)
        {
            if (level < 0)
                throw new ArgumentOutOfRangeException(nameof(level));

            var rootUrls = await GetPageLinksAsync(request);

            if (level == 0)
                return rootUrls;

            var links = await GetAllPagesLinks(rootUrls);

            --level;
            var tasks = await Task.WhenAll(links.Select(link => GetLinksAsync(request, level)));
            return tasks.SelectMany(l => l);
        }

        private async Task<IEnumerable<string>> GetPageLinksAsync(DotnetCrawlerRequest request)
        {
            try
            {
                var htmlDocument = await _webClientService.FromWebAsync(request.Url);

                IEnumerable<string> links = ProcessLinks(htmlDocument);

                links = FilterByRegularExpression(request, links);

                return links;
            }
            catch (Exception)
            {
                return Enumerable.Empty<string>();
            }
        }

        private static IEnumerable<string> FilterByRegularExpression(DotnetCrawlerRequest request, IEnumerable<string> links)
        {
            if (!string.IsNullOrWhiteSpace(request.Regex))
            {
                var regex = new Regex(request.Regex);

                if (regex != null)
                    links = links.Where(x => regex.IsMatch(x));
            }

            return links;
        }

        private static IEnumerable<string> ProcessLinks(HtmlDocument htmlDocument)
        {
            return htmlDocument.DocumentNode
                .Descendants("a")
                .Select(a => a.GetAttributeValue("href", null))
                .Where(u => !string.IsNullOrEmpty(u))
                .Distinct();
        }

        private async Task<IEnumerable<string>> GetAllPagesLinks(IEnumerable<string> rootUrls)
        {
            var result = await Task.WhenAll(rootUrls.Select(url => GetPageLinksAsync(new DotnetCrawlerRequest() { Url = url })));

            return result.SelectMany(x => x).Distinct();
        }
    }
}
