﻿using HtmlAgilityPack;
using System.Threading.Tasks;

namespace DotnetCrawler.Downloader
{
    public interface IDotnetCrawlerDownloader
    {
        Task<HtmlDocument> Download(string crawlUrl);
    }
}
