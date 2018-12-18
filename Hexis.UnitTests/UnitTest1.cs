using Hexis.ApplicationLayer;
using Hexis.DomainModelLayer;
using Hexis.Dto;
using Hexis.InfrastructureLayer;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Category_InsertAsync_DuplicateName()
        {
            //Arrange
            var mockRepo = new Mock<ICategoryRepository>();
            mockRepo.Setup(m => m.GetByNameAsync(It.IsAny<string>())).Returns(Task.FromResult(new Category()));

            var mockUnit = new Mock<IUnitOfWork>();
            mockUnit.Setup(x => x.SetCategoryRepository()).Returns(mockRepo.Object);

            var categoryDto = new CategoryDto()
            {
                Name = "Teste"
            };

            var service = new CategoryService(mockUnit.Object);

            //Act / Assert
            Assert.ThrowsAsync<ApplicationException>(() => service.InsertAsync(categoryDto),
                "There is already a category with the name Teste.");
        }

        [Test]
        public void Category_InsertAsync_Default()
        {
            //Arrange
            var mockRepo = new Mock<ICategoryRepository>();
            mockRepo.Setup(m => m.GetByNameAsync(It.IsAny<string>())).Returns(Task.FromResult<Category>(null));
            mockRepo.Setup(m => m.Insert(It.IsAny<Category>())).Verifiable(); ;            

            var mockUnit = new Mock<IUnitOfWork>();
            mockUnit.Setup(x => x.SetCategoryRepository()).Returns(mockRepo.Object);
            mockUnit.Setup(x => x.SaveAsync()).Returns(Task.FromResult<int>(1));

            var service = new CategoryService(mockUnit.Object);

            //Act
            var result = service.InsertAsync(new CategoryDto());

            //Assert
            Assert.AreEqual(result.Result, 1);            
        }


        [Test]
        public void Product_IncreaseStockAsync_Default()
        {
            //Arrange
            var mockRepo = new Mock<IProductRepository>();
            mockRepo.Setup(m => m.GetByIdAsync(It.IsAny<int>())).Returns(Task.FromResult<Product>(new Product() { Stock = 10 }));
            mockRepo.Setup(m => m.Update(It.IsAny<Product>())).Verifiable(); ;

            var mockUnit = new Mock<IUnitOfWork>();
            mockUnit.Setup(x => x.SetProductRepository()).Returns(mockRepo.Object);
            mockUnit.Setup(x => x.SaveAsync()).Returns(Task.FromResult<int>(1));

            var service = new ProductService(mockUnit.Object);

            //Act
            var result = service.IncreaseStockAsync(1, 10);

            //Assert
            Assert.AreEqual(result.Result, 1);
        }
    }
}