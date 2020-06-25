using DotnetCrawler.Data.Attributes;
using DotnetCrawler.Data.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DotnetCrawler.Data.Models.Clarteys
{
    [DotnetCrawlerEntity(XPath = "//*[@class='offer__info']")]
    public partial class ClarteysApartment : IEntity
    {
        public Guid Id { get; set; }

        [DotnetCrawlerField(Expression = "//li[7]/div[2]/b/text()", SelectorType = SelectorType.XPath)]
        public string StartingPrice { get; set; }

        [DotnetCrawlerField(Expression = "//li[5]/div[2]/a/text()", SelectorType = SelectorType.XPath)]
        public string Address { get; set; }

        [DotnetCrawlerField(Expression = "//li[4]/div[2]/b/text()", SelectorType = SelectorType.XPath)]
        public string Rooms { get; set; }

        [DotnetCrawlerField(Expression = "//li[3]/div[2]/b/text()", SelectorType = SelectorType.XPath)]
        public string Area { get; set; }
    }
}