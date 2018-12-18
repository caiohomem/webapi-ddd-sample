using Hexis.DomainModelLayer;
using Hexis.Dto;
using Hexis.InfrastructureLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hexis.ApplicationLayer
{
    public class ProductService : IProductService
    {
        private IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private Product ConvertFromDtoToModel(ProductDto productDto)
        => new Product
        {
            Id = productDto.Id,
            Name = productDto.Name,
            CategoryId = productDto.CategoryId,
            Description = productDto.Description,
            Price = productDto.Price,
            Stock = productDto.Stock,
            Timestamp = productDto.Timestamp
        };

        private ProductDto ConvertFromModelToDto(Product product)
        => new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            CategoryId = product.CategoryId,
            Description = product.Description,
            Price = product.Price,
            Stock = product.Stock,
            Timestamp = product.Timestamp
        };

        //Insert a new product
        //This will insert a new product always with 0 stock.
        //You cannot insert products with the same name.
        //Stock will always be 0 when inserting a new product.
        public async Task<int> InsertAsync(ProductDto productDto)
        {
            var product = ConvertFromDtoToModel(productDto);            

            var productDuplicatedName = await _unitOfWork.SetProductRepository().GetByNameAsync(product.Name);
            if (productDuplicatedName != null)
                throw new ApplicationException($"There is already a product with the name {productDuplicatedName.Name}.");

            var categoryNorFound = await _unitOfWork.SetCategoryRepository().GetByIdAsync(product.CategoryId);
            if (categoryNorFound == null)
                throw new ApplicationException($"Category with Id {product.CategoryId} not found.");

            product.Stock = 0;

            _unitOfWork.SetProductRepository().Insert(product);
            return await _unitOfWork.SaveAsync();
        }

        //Update a new product
        //All fields are updatable except the Id and Stock
        //Name cannot be duplicate with another already in the system
        //The product id will be used as key 
        public async Task<int> UpdateAsync(ProductDto productDto)
        {
            try
            {
                var product = ConvertFromDtoToModel(productDto);

                var productNotFound = await _unitOfWork.SetProductRepository().GetByIdAsync(product.Id);
                if (productNotFound == null)
                    throw new ApplicationException($"Product with Id {product.Id} not found.");

                var productDuplicatedName = await _unitOfWork.SetProductRepository().GetByNameAsync(product.Name);
                if (productDuplicatedName != null && productDuplicatedName?.Id != product.Id)
                    throw new ApplicationException($"There is already a product with the name {productDuplicatedName.Name}.");

                product.Stock = productNotFound.Stock;

                _unitOfWork.SetProductRepository().Update(product);
                return await _unitOfWork.SaveAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new ApplicationException($"This product has been changed by another user.");
            }

        }


        //Increase / Decrease stock
        //This will increase or decrease the stock of a product
        //The product id will be used as key 
        public async Task<int> IncreaseStockAsync(int id, int quantity)
        {
            var product = await _unitOfWork.SetProductRepository().GetByIdAsync(id);

            if (product == null)
                throw new ApplicationException($"Category with Id {id} not found.");

            product.Stock = product.Stock + quantity;

            _unitOfWork.SetProductRepository().Update(product);
            return await _unitOfWork.SaveAsync();
        }

        public async Task<int> DecreaseStockAsync(int id, int quantity)
        {
            var product = await _unitOfWork.SetProductRepository().GetByIdAsync(id);

            if (product == null)
                throw new ApplicationException($"Category with Id {id} not found.");

            if (product.Stock - quantity < 0)
                throw new ApplicationException($"You have only {product.Stock} products in the stock.");

            product.Stock = product.Stock - quantity;            

            _unitOfWork.SetProductRepository().Update(product);
            return await _unitOfWork.SaveAsync();
        }

        //Remove a product o This will remove a product
        //The product id will be used as key 
        public async Task<int> RemoveAsync(int id)
        {
            var product = await _unitOfWork.SetProductRepository().GetByIdAsync(id);

            if (product == null)
                throw new ApplicationException($"Product with Id {id} not found.");

            _unitOfWork.SetProductRepository().Remove(product);
            return await _unitOfWork.SaveAsync();
        }

        //Get all products
        //This will list all products paged(this method will receive the page size and page index)
        public async Task<IEnumerable<ProductDto>> GetAllPagedAsync(int pageSize, int pageIndex)
        {
            var list = await _unitOfWork.SetProductRepository().GetAllAsync();
            var pagedList = list.Skip((pageIndex - 1) * pageSize).Take(pageSize);

            return pagedList.ToList().ConvertAll(x => ConvertFromModelToDto(x));
        }
    }
}
