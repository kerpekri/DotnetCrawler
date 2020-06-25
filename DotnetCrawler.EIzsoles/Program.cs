using DotnetCrawler.Core;
using DotnetCrawler.Data.Models.EIzsoles;
using DotnetCrawler.Downloader;
using DotnetCrawler.Pipeline;
using DotnetCrawler.Processor;
using DotnetCrawler.Request;
using System.IO;
using System.Threading.Tasks;

namespace DotnetCrawler.EIzsoles
{
    public class Program
    {
        public const string RootUrl = "https://izsoles.ta.gov.lv/";
        public const string DownloadPath = "C:/DotnetCrawlercrawler/EIzsoles";
        public const string RegExp = @".*izsole/.+";

        public static void Main(string[] args)
        {
            MainAsync(args).Wait();
        }

        public static async Task MainAsync(string[] args)
        {
            CreateRootDirectory();

            DotnetCrawler<EIzsolesThing> crawler = SetupCrawler();

            await crawler.Crawle();
        }

        private static DotnetCrawler<EIzsolesThing> SetupCrawler()
        {
            return new DotnetCrawler<EIzsolesThing>()
                .AddRequest(new DotnetCrawlerRequest { Url = RootUrl, Regex = RegExp, TimeOut = 10000 })
                .AddDownloader(new DotnetCrawlerDownloader { DownloderType = DotnetCrawlerDownloaderType.FromWeb, DownloadPath = DownloadPath })
                .AddProcessor(new DotnetCrawlerProcessor<EIzsolesThing> { })
                .AddPipeline(new DotnetCrawlerPipeline<EIzsolesThing> { });
        }

        private static void CreateRootDirectory()
        {
            Directory.CreateDirectory(DownloadPath);
        }
    }
}