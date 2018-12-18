using Hexis.DomainModelLayer;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hexis.InfrastructureLayer
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {

        public ProductRepository(HexisContext hexisContext ) : base(hexisContext)
        {
        }

        public virtual async Task<Product> GetByNameAsync(string name)
        {
            return await _context.Set<Product>().FirstOrDefaultAsync(x => x.Name == name);
        }

        public virtual async Task<IEnumerable<Product>> GetByCategoryIdAsync(int id)
        {
            return await _context.Set<Product>().Where(x => x.CategoryId == id).ToListAsync();
        }
    }
}
