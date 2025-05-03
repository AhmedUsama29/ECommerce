using Domain.Contracs;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class GenericRepository<TEntity, TKey>(StoreDbContext _storeDbContext) : IGenericRepository<TEntity, TKey> 
                                                    where TEntity : BaseEntity<TKey>
    {
        public void Add(TEntity entity)
        {
            _storeDbContext.Set<TEntity>().Add(entity);
        }

        public void Update(TEntity entity)
        {
            _storeDbContext.Set<TEntity>().Update(entity);
        }

        public void Delete(TEntity entity)
        {
            _storeDbContext.Set<TEntity>().Remove(entity);
        }

        public async Task<TEntity> GetByIdAsync(ISpecifications<TEntity> specifications)
        {

            var res = await SpecificationEvaluator
                    .CreateQuery(_storeDbContext.Set<TEntity>(), specifications)
                    .FirstOrDefaultAsync();

            return res;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity> specifications)
        {
            var res = await SpecificationEvaluator
                    .CreateQuery(_storeDbContext.Set<TEntity>(), specifications)
                    .ToListAsync();

            return res;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync() //remove later
        {
            return await _storeDbContext.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(TKey id) //remove later
        {
            return await _storeDbContext.Set<TEntity>().FindAsync(id);
        }


    }
}
