using DotnetCrawler.Core;
using DotnetCrawler.Data.Models;
using DotnetCrawler.Downloader;
using DotnetCrawler.Pipeline;
using DotnetCrawler.Processor;
using DotnetCrawler.Request;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetCrawler.Sample
{
    class Program
    {
        private static readonly string dummyUrl = "https://clarteys.lv/";

        static void Main(string[] args)
        {
            MainAsync(args).Wait();
        }

        static async Task MainAsync(string[] args)
        {
            var crawler = new DotnetCrawler<Catalog>()
                .AddRequest(new DotnetCrawlerRequest { Url = dummyUrl, Regex = @".*izsole/.+", TimeOut = 5000 })
                .AddDownloader(new DotnetCrawlerDownloader { DownloderType = DotnetCrawlerDownloaderType.FromFile, DownloadPath = @"C:\DotnetCrawlercrawler\" })
                .AddProcessor(new DotnetCrawlerProcessor<Catalog> { })
                .AddPipeline(new DotnetCrawlerPipeline<Catalog> { });

            await crawler.Crawle();
        }
    }
}
