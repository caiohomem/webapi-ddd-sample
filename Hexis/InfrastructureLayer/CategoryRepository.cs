using Hexis.DomainModelLayer;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Hexis.InfrastructureLayer
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(HexisContext hexisContext) : base(hexisContext)
        {
        }

        public virtual async Task<Category> GetByNameAsync(string name)
        {
            return await _context.Set<Category>().FirstOrDefaultAsync(x => x.Name == name);
        }
    }
}
