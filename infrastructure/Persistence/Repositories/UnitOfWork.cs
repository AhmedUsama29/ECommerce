using Domain.Contracs;
using Domain.Models;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class UnitOfWork(StoreDbContext _storeDbContext) : IUnitOfWork
    {

        private readonly Dictionary<string, object> _repositories = [];
        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {

            var typeName = typeof(TEntity).Name;
            if (_repositories.ContainsKey(typeName))
                return (GenericRepository<TEntity, TKey>) _repositories[typeName];


            var repo = new GenericRepository<TEntity, TKey>(_storeDbContext);

            _repositories[typeName] = repo;

            return repo;
        }

        public async Task<int> SaveChanges()
        {
            return await _storeDbContext.SaveChangesAsync();
        }
    }
}
