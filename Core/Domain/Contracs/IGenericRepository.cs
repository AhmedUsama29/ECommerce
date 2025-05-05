using Domain.Models;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracs
{
    public interface IGenericRepository<TEntity,TKey> where TEntity : BaseEntity<TKey>
    {

        Task<int> CountAsync(ISpecifications<TEntity> specifications);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);

        Task<TEntity?> GetByIdAsync(TKey id);   //remove later

        Task<IEnumerable<TEntity>> GetAllAsync();   //remove later

        Task<TEntity> GetByIdAsync(ISpecifications<TEntity> specifications);

        Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity> specifications);

    }
}
