using System;
using System.Collections.Generic;
using System.Text;

namespace DotnetCrawler.Data.Repository
{
    public interface IEntity
    {
        Guid Id { get; }
    }
}
