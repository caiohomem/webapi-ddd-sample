using System.Collections.Generic;
using System.Threading.Tasks;
using Hexis.DomainModelLayer;

namespace Hexis.InfrastructureLayer
{
    public interface IGenericRepository<TEntity> where TEntity : EntityBase
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(int id);
        void Insert(TEntity entity);
        void Remove(TEntity entity);
        void Update(TEntity entity);
    }
}