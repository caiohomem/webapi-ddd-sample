using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexis.InfrastructureLayer
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HexisContext _hexisContext;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductRepository _productRepository;

        public UnitOfWork(HexisContext hexisContext, ICategoryRepository categoryRepository, IProductRepository productRepository)
        {
            _hexisContext = hexisContext;
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
        }

        public ICategoryRepository SetCategoryRepository()
        {
            return _categoryRepository;
        }

        public IProductRepository SetProductRepository()
        {
            return _productRepository;
        }

        public async Task<int> SaveAsync()
        {
            return await _hexisContext.SaveChangesAsync();
        }
    }
}
