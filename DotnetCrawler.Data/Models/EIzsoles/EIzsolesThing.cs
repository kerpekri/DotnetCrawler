using DotnetCrawler.Data.Attributes;
using DotnetCrawler.Data.Repository;
using System;

namespace DotnetCrawler.Data.Models.EIzsoles
{
    [DotnetCrawlerEntity(XPath = "//*[@class='object-info-row']")]
    public partial class EIzsolesThing : IEntity
    {
        public Guid Id { get; set; }

        [DotnetCrawlerField(Expression = "//*[@class='object-title']//text()", SelectorType = SelectorType.XPath)]
        public string Name { get; set; }

        //[DotnetCrawlerField(Expression = "//li[5]/div[2]/a/text()", SelectorType = SelectorType.XPath)]
        //public string Address { get; set; }

        //[DotnetCrawlerField(Expression = "//li[4]/div[2]/b/text()", SelectorType = SelectorType.XPath)]
        //public string Rooms { get; set; }

        //[DotnetCrawlerField(Expression = "//li[3]/div[2]/b/text()", SelectorType = SelectorType.XPath)]
        //public string Area { get; set; }
    }
}
