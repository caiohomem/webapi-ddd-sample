using Hexis.DomainModelLayer;
using Hexis.Dto;
using Hexis.InfrastructureLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexis.ApplicationLayer
{
    public class CategoryService : ICategoryService
    {
        private IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private Category ConvertFromDtoToModel(CategoryDto categoryDto)
        => new Category
        {
            Id = categoryDto.Id,
            Name = categoryDto.Name
        };        

        private CategoryDto ConvertFromModelToDto(Category category) 
        => new CategoryDto
        {
            Id = category.Id,
            Name = category.Name
        };
        

        //Insert a new category 
        //This will insert a new category.
        //You cannot insert categories with the same name. 
        public async Task<int> InsertAsync(CategoryDto categoryDto)
        {
            var category = ConvertFromDtoToModel(categoryDto);

            var categoryDuplicatedName = await _unitOfWork.SetCategoryRepository().GetByNameAsync(category.Name);
            if (categoryDuplicatedName != null)
                throw new ApplicationException($"There is already a category with the name {categoryDuplicatedName.Name}.");

            _unitOfWork.SetCategoryRepository().Insert(category);
            return await _unitOfWork.SaveAsync();
        }

        //Update category 
        //You can only change the name of the category 
        //Name cannot be duplicate with another already in the system 
        //The category id will be used as key 
        public async Task<int> UpdateAsync(CategoryDto categoryDto)
        {
            var category = ConvertFromDtoToModel(categoryDto);

            var categoryNorFound = await _unitOfWork.SetCategoryRepository().GetByIdAsync(category.Id);
            if (categoryNorFound == null)
                throw new ApplicationException($"Category with Id {category.Id} not found.");

            var categoryDuplicatedName = await _unitOfWork.SetCategoryRepository().GetByNameAsync(category.Name);
            if (categoryDuplicatedName?.Id == category.Id)
                throw new ApplicationException($"You are trying to change the category to the same name.");

            if (categoryDuplicatedName != null)
                throw new ApplicationException($"There is already a category with the name {categoryDuplicatedName.Name}.");

            _unitOfWork.SetCategoryRepository().Update(category);
            return await _unitOfWork.SaveAsync();
        }

        //Remove a category 
        //You can only remove empty categories(with no associated products) 
        public async Task<int> RemoveAsync(int id)
        {
            var category = await _unitOfWork.SetCategoryRepository().GetByIdAsync(id);
            if (category == null)
                throw new ApplicationException($"Category with Id {id} not found.");

            var product = await _unitOfWork.SetProductRepository().GetByCategoryIdAsync(id);
            if(product?.Count() > 0)
                throw new ApplicationException($"There are products associated with this category.");

            _unitOfWork.SetCategoryRepository().Remove(category);
            return await _unitOfWork.SaveAsync();
        }

        //Get all categories 
        //This will list all categories paged(this method will receive the page size and page index)
        public async Task<IEnumerable<CategoryDto>> GetAllPagedAsync(int pageSize, int pageIndex)
        {
            var list = await _unitOfWork.SetCategoryRepository().GetAllAsync();
            var pagedList = list.Skip((pageIndex - 1) * pageSize).Take(pageSize);

            return pagedList.ToList().ConvertAll(x => ConvertFromModelToDto(x));
        }
    }
}
