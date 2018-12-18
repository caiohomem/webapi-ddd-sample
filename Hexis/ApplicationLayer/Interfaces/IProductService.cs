using System.Collections.Generic;
using System.Threading.Tasks;
using Hexis.Dto;

namespace Hexis.ApplicationLayer
{
    public interface IProductService
    {
        Task<int> DecreaseStockAsync(int id, int quantity);
        Task<int> IncreaseStockAsync(int id, int quantity);
        Task<IEnumerable<ProductDto>> GetAllPagedAsync(int pageSize, int pageIndex);
        Task<int> InsertAsync(ProductDto category);
        Task<int> RemoveAsync(int id);
        Task<int> UpdateAsync(ProductDto category);
    }
}