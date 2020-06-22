using DotnetCrawler.Data.Attributes;
using DotnetCrawler.Data.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DotnetCrawler.Data.Models
{
    //[DotnetCrawlerEntity(XPath = "//*[@id='offer__info']/div[1]")]
    public partial class Apartment : IEntity
    {
        public Guid Id { get; set; }
        public string StartingPrice { get; set; }
        public string Address { get; set; }
        public string Rooms { get; set; }
        public string Area { get; set; }

        //[DotnetCrawlerField(Expression = "1", SelectorType = SelectorType.FixedValue)]
        //public int CatalogBrandId { get; set; }

        //[DotnetCrawlerField(Expression = "", SelectorType = SelectorType.CssSelector)]
        //public string Name { get; set; }

        //public virtual CatalogBrand CatalogBrand { get; set; }
        //public virtual CatalogType CatalogType { get; set; }
    }
}
