using DotnetCrawler.Downloader;
using DotnetCrawler.Downloader.Implementations;
using FluentAssertions;
using HtmlAgilityPack;
using Moq;
using Xunit;

namespace DotnetCrawler.Tests
{
    public class DownloaderShould
    {
        [Fact]
        public async void Download_ReturnsEmptyDocument()
        {
            HtmlDocument emptyDocument = new HtmlDocument();

            Mock<IWebClientService> webClientMock = new Mock<IWebClientService>();
            webClientMock.Setup(a => a.FromWebAsync(It.IsAny<string>())).ReturnsAsync(emptyDocument);

            DotnetCrawlerDownloader downloader = new DotnetCrawlerDownloader(webClientMock.Object);
            HtmlDocument result = await downloader.Download(It.IsAny<string>());

            result.Should().Be(emptyDocument);
            webClientMock.Verify(m => m.FromWebAsync(It.IsAny<string>()), Times.Exactly(1));
        }

        [Fact]
        public async void Download_ReturnsNotEmptyDocument()
        {
            HtmlDocument emptyDocument = new HtmlDocument();

            string html = DummyHtml();

            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            Mock<IWebClientService> webClientMock = new Mock<IWebClientService>();
            webClientMock.Setup(a => a.FromWebAsync(It.IsAny<string>())).ReturnsAsync(htmlDocument);

            DotnetCrawlerDownloader downloader = new DotnetCrawlerDownloader(webClientMock.Object);
            HtmlDocument result = await downloader.Download(It.IsAny<string>());

            result.Should().NotBe(emptyDocument);
            webClientMock.Verify(m => m.FromWebAsync(It.IsAny<string>()), Times.Exactly(1));
        }

        [Fact]
        public async void Download_ReturnsHeadingText()
        {
            HtmlDocument emptyDocument = new HtmlDocument();

            string html = DummyHtml();

            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            Mock<IWebClientService> webClientMock = new Mock<IWebClientService>();
            webClientMock.Setup(a => a.FromWebAsync(It.IsAny<string>())).ReturnsAsync(htmlDocument);

            DotnetCrawlerDownloader downloader = new DotnetCrawlerDownloader(webClientMock.Object);
            HtmlDocument result = await downloader.Download(It.IsAny<string>());

            string headingText = result.DocumentNode.SelectSingleNode("//body/h1/text()").InnerText;

            headingText.Should().Be("First heading");
            webClientMock.Verify(m => m.FromWebAsync(It.IsAny<string>()), Times.Exactly(1));
        }

        private static string DummyHtml()
        {
            return @"<!DOCTYPE html>
                <html>
                <body>
                    <h1 id='first'>First heading</h1>
                    <h2 id='second'>Second heading</h2>
                    <p class='paragraph'>Simple paragraph</p>
                </body>
                </html> ";
        }
    }
}
