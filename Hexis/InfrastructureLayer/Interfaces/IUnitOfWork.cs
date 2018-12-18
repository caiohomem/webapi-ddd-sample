using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexis.InfrastructureLayer
{
    public interface IUnitOfWork
    {
        ICategoryRepository SetCategoryRepository();
        IProductRepository SetProductRepository();
        Task<int> SaveAsync();
    }
}
