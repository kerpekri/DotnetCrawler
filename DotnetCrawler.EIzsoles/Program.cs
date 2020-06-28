using DotnetCrawler.Core;
using DotnetCrawler.Data.Models.EIzsoles;
using DotnetCrawler.Downloader.Implementations;
using DotnetCrawler.Pipeline;
using DotnetCrawler.Processor;
using DotnetCrawler.Request;
using System.Threading.Tasks;

namespace DotnetCrawler.EIzsoles
{
    public class Program
    {
        public const string RootUrl = "https://izsoles.ta.gov.lv/";
        public const string RegExp = @".*izsole/.+";

        public static void Main(string[] args)
        {
            MainAsync(args).Wait();
        }

        public static async Task MainAsync(string[] args)
        {
            DotnetCrawler<EIzsolesThing> crawler = SetupCrawler();

            await crawler.Crawle();
        }

        private static DotnetCrawler<EIzsolesThing> SetupCrawler()
        {
            return new DotnetCrawler<EIzsolesThing>()
                .AddRequest(new DotnetCrawlerRequest { Url = RootUrl, Regex = RegExp, TimeOut = 10000 })
                .AddDownloader(new DotnetCrawlerDownloader { })
                .AddProcessor(new DotnetCrawlerProcessor<EIzsolesThing> { })
                .AddPipeline(new DotnetCrawlerPipeline<EIzsolesThing> { });
        }
    }
}