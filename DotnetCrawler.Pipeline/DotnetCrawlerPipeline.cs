using DotnetCrawler.Data.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotnetCrawler.Pipeline
{
    public class DotnetCrawlerPipeline<TEntity> : IDotnetCrawlerPipeline<TEntity> where TEntity : class, IEntity
    {
        private readonly IGenericRepository<TEntity> _repository;

        public DotnetCrawlerPipeline()
        {
            _repository = new GenericRepository<TEntity>();
        }

        public async Task Run(IEnumerable<TEntity> entityList)
        {
            foreach (TEntity entity in entityList)
            {
                if (await _repository.GetByAddressAndPrice(entity.Price, entity.Address) == false)
                {
                    await _repository.CreateAsync(entity);
                }
            }
        }
    }
}
