using DotnetCrawler.Core;
using DotnetCrawler.Data.Models;
using DotnetCrawler.Downloader;
using DotnetCrawler.Pipeline;
using DotnetCrawler.Processor;
using DotnetCrawler.Request;
using System.Threading.Tasks;

namespace DotnetCrawler.Sample
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
            var crawler = new DotnetCrawler<Apartment>()
                .AddRequest(new DotnetCrawlerRequest { Url = ApartmentUrl, Regex = RegExp, TimeOut = 5000 })
                .AddDownloader(new DotnetCrawlerDownloader { DownloderType = DotnetCrawlerDownloaderType.FromFile, DownloadPath = @"C:\DotnetCrawlercrawler\" })
                .AddProcessor(new DotnetCrawlerProcessor<Apartment> { })
                .AddPipeline(new DotnetCrawlerPipeline<Apartment> { });

            await crawler.Crawle();
        }
    }
}
