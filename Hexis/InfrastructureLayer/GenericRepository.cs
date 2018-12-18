using Hexis.DomainModelLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexis.InfrastructureLayer
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : EntityBase
    {
        protected readonly HexisContext _context;

        public GenericRepository(HexisContext hexisContext)
        {
            _context = hexisContext;
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {

            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _context.Set<TEntity>()
                        .AsNoTracking()
                        .FirstOrDefaultAsync(e => e.Id == id);
        }

        public void Insert(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }

        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }

        public void Remove(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }
    }
}
