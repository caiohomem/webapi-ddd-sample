using Hexis.DomainModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexis.InfrastructureLayer
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<Product> GetByNameAsync(string name);
        Task<IEnumerable<Product>> GetByCategoryIdAsync(int id);
    }
}
