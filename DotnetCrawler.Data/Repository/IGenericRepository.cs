using System;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetCrawler.Data.Repository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetAll();
        Task<TEntity> GetById(Guid id);
        Task CreateAsync(TEntity entity);
        Task Update(int id, TEntity entity);
        Task Delete(Guid id);
        Task<bool> GetByAddressAndPrice(string price, string address);
    }
}
