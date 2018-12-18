using System.Collections.Generic;
using System.Threading.Tasks;
using Hexis.Dto;

namespace Hexis.ApplicationLayer
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllPagedAsync(int pageSize, int pageIndex);
        Task<int> InsertAsync(CategoryDto category);
        Task<int> RemoveAsync(int id);
        Task<int> UpdateAsync(CategoryDto category);
    }
}