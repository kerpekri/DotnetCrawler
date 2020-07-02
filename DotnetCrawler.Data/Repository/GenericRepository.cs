using DotnetCrawler.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetCrawler.Data.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly CrawlerContext _dbContext;
        private readonly DbSet<TEntity> _entities;

        public GenericRepository()
        {
            _dbContext = new CrawlerContext();
            _entities = _dbContext.Set<TEntity>();
        }

        public IQueryable<TEntity> GetAll()
        {
            return _entities.AsNoTracking();
        }

        public async Task<TEntity> GetById(Guid id)
        {
            return await _entities
                        .AsNoTracking()
                        .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task CreateAsync(TEntity entity)
        {
            await _entities.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(int id, TEntity entity)
        {
            _entities.Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var entity = await GetById(id);
            _entities.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> GetByAddressAndPrice(string price, string address)
        {
            return await _entities
                        .AsNoTracking()
                        .AnyAsync(e => e.Price == price && e.Address == address);
        }
    }
}
