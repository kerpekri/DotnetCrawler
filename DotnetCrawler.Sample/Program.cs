using DotnetCrawler.Core;
using DotnetCrawler.Data.Models.Clarteys;
using DotnetCrawler.Downloader.Implementations;
using DotnetCrawler.Pipeline;
using DotnetCrawler.Processor;
using DotnetCrawler.Request;
using System.Threading.Tasks;

namespace DotnetCrawler.Clarteys
{
    public class Program
    {
        public const string LandUrl = "https://clarteys.lv/izsoles?ni_type=1&type=ni";
        public const string HouseUrl = "https://clarteys.lv/izsoles?ni_type=2&type=ni";
        public const string ApartmentUrl = "https://clarteys.lv/izsoles?ni_type=3&type=ni";
        public const string RegExp = @".*izsole/.+";

        public static void Main(string[] args)
        {
            MainAsync(args).Wait();
        }

        public static async Task MainAsync(string[] args)
        {
            var crawler = new DotnetCrawler<ClarteysApartment>()
                .AddRequest(new DotnetCrawlerRequest { Url = ApartmentUrl, Regex = RegExp, TimeOut = 5000 })
                .AddDownloader(new DotnetCrawlerDownloader { })
                .AddProcessor(new DotnetCrawlerProcessor<ClarteysApartment> { })
                .AddPipeline(new DotnetCrawlerPipeline<ClarteysApartment> { });

            await crawler.Crawle();
        }
    }
}
