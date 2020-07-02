using System;

namespace DotnetCrawler.Data.Repository
{
    public interface IEntity
    {
        Guid Id { get; }
        string Address { get; }
        string Price { get; }
    }
}
