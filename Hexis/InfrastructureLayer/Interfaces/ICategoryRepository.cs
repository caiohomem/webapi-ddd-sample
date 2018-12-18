using Hexis.DomainModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexis.InfrastructureLayer
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<Category> GetByNameAsync(string name);
    }
}
