using DotnetCrawler.Data.Attributes;
using DotnetCrawler.Data.Repository;
using DotnetCrawler.Processor;
using FluentAssertions;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using Xunit;

namespace DotnetCrawler.UnitTests
{
    public class ProcessorShould
    {
        [Fact]
        public async void Process_ReturnsOneEntity()
        {
            string html = DummyHtml();

            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            DotnetCrawlerProcessor<EIzsolesThing2> processor = new DotnetCrawlerProcessor<EIzsolesThing2>();
            IEnumerable<EIzsolesThing2> result = await processor.Process(htmlDocument);

            result.Should().NotBeEmpty().And.HaveCount(1);
        }

        private static string DummyHtml()
        {
            return @"<!DOCTYPE html>
                <html>
                <body>
                    <div class='info-row'>
                        <div class='title'>Some text 1</div>
                    </div>
                </body>
                </html> ";
        }
    }

    [DotnetCrawlerEntity(XPath = "//*[@class='info-row']")]
    public partial class EIzsolesThing2 : IEntity
    {
        public Guid Id { get; set; }

        [DotnetCrawlerField(Expression = "//*[@class='title']//text()", SelectorType = SelectorType.XPath)]
        public string Name { get; set; }
    }
}
