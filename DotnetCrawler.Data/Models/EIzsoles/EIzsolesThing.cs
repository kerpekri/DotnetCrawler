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

        public string Address { get; set; }
        public string Price { get; set; }
    }
}
