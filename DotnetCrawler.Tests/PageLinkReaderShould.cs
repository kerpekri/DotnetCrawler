using DotnetCrawler.Downloader;
using DotnetCrawler.Request;
using FluentAssertions;
using HtmlAgilityPack;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace DotnetCrawler.Tests
{
    public class PageLinkReaderShould
    {
        [Fact]
        public async void GetLinksAsync_ReturnsNoLinks()
        {
            DotnetCrawlerRequest request = new DotnetCrawlerRequest();
            HtmlDocument htmlDocument = new HtmlDocument();

            Mock<IWebClientService> webClientMock = new Mock<IWebClientService>();
            webClientMock.Setup(a => a.FromWebAsync(It.IsAny<string>())).ReturnsAsync(htmlDocument);

            DotnetCrawlerPageLinkReader linkReader = new DotnetCrawlerPageLinkReader(webClientMock.Object);
            IEnumerable<string> links = await linkReader.GetLinksAsync(request);

            links.Should().BeEmpty();
            webClientMock.Verify(m => m.FromWebAsync(It.IsAny<string>()), Times.Exactly(1));
        }

        [Fact]
        public void GetLinksAsync_ThrowsArgumentException()
        {
            DotnetCrawlerRequest request = new DotnetCrawlerRequest();
            HtmlDocument htmlDocument = new HtmlDocument();

            Mock<IWebClientService> webClientMock = new Mock<IWebClientService>();
            webClientMock.Setup(a => a.FromWebAsync(It.IsAny<string>())).ReturnsAsync(htmlDocument);

            DotnetCrawlerPageLinkReader linkReader = new DotnetCrawlerPageLinkReader(webClientMock.Object);

            linkReader.Invoking(y => y.GetLinksAsync(request, -1))
                .Should().Throw<ArgumentOutOfRangeException>()
                .Where(e => e.Message.StartsWith("Specified argument was out of the range"));
        }

        [Fact]
        public async void GetLinksAsync_ReturnsAllLinks()
        {
            string html = DummyHtml();

            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            DotnetCrawlerRequest request = new DotnetCrawlerRequest();

            Mock<IWebClientService> webClientMock = new Mock<IWebClientService>();
            webClientMock.Setup(a => a.FromWebAsync(It.IsAny<string>())).ReturnsAsync(htmlDocument);

            DotnetCrawlerPageLinkReader linkReader = new DotnetCrawlerPageLinkReader(webClientMock.Object);
            IEnumerable<string> links = await linkReader.GetLinksAsync(request);

            links.Should().NotBeEmpty().And.HaveCount(3).And.ContainItemsAssignableTo<string>();
            webClientMock.Verify(m => m.FromWebAsync(It.IsAny<string>()), Times.Exactly(1));
        }

        [Fact]
        public async void GetLinksAsync_ReturnsLink_BasedOnRegularExpression()
        {
            string html = DummyHtml();

            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            DotnetCrawlerRequest request = new DotnetCrawlerRequest() { Regex = "magic" };

            Mock<IWebClientService> webClientMock = new Mock<IWebClientService>();
            webClientMock.Setup(a => a.FromWebAsync(It.IsAny<string>())).ReturnsAsync(htmlDocument);

            DotnetCrawlerPageLinkReader linkReader = new DotnetCrawlerPageLinkReader(webClientMock.Object);
            IEnumerable<string> links = await linkReader.GetLinksAsync(request);

            links.Should().NotBeEmpty().And.HaveCount(1).And.ContainItemsAssignableTo<string>();
            webClientMock.Verify(m => m.FromWebAsync(It.IsAny<string>()), Times.Exactly(1));
        }

        private static string DummyHtml()
        {
            return @"<!DOCTYPE html>
                <html>
                <body>
                    <div class='name'>
                       <a href='https://test.url/100'>Some text 1</a>
                    </div>
                    <div class='name'>
                       <a href='https://test.url/101'>Some text 2</a>
                    </div>
                    <div class='name'>
                       <a href='https://magic.url/101'>Some text 2</a>
                    </div>
                </body>
                </html> ";
        }
    }
}
