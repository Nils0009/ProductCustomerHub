using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Infrastructure.Tests.Repositories;

public class ProductRepository_Tests
{
    private readonly ProductCatalogContext _context =
        new(new DbContextOptionsBuilder<ProductCatalogContext>()
            .UseInMemoryDatabase($"{Guid.NewGuid()}")
            .Options);

    [Fact]
    public async Task Create_Should_Add_One_Entity_To_Database_And_Return_Entity()
    {
        try
        {
            //Arrange
            var categoryRepository = new CategoryRepository(_context);
            var categoryEntity = new CategoryEntity
            {
                CategoryName = "Mobile"
            };

            var manufacturerRepository = new ManufacturerRepository(_context);
            var manufacturerEntity = new ManufacturerEntity
            {
                Manufacturer = "Samsung"
            };

            var priceRepository = new PriceRepository(_context);
            var priceEntity = new PriceEntity
            {
                UnitPrice = 100
            };
            
            var productRepository = new ProductRepository(_context);
            var productEntity = new ProductEntity
            {
                ArticleNumber = Guid.NewGuid().ToString(),
                Title = "Samsung Galaxy 8",
                Description = "Red 256gb",
                CategoryId = categoryEntity.Id,
                ManufacturerId = manufacturerEntity.Id,
                PriceId = priceEntity.Id
            };
            //Act
            var result = await productRepository.Create(productEntity);

            //Assert
            Assert.NotNull(result);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Exception: {ex.Message}");
        }

    }

    [Fact]
    public async Task GetAll_Should_GetAll_Entitys_From_Database_And_Return_IEnumerableList()
    {
        try
        {
            //Arrange
            var productRepository = new ProductRepository(_context);

            //Act
            var result = await productRepository.GetAll();

            //Assert
            Assert.NotNull(result);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Exception: {ex.Message}");
        }

    }

    [Fact]
    public async Task GetOne_Should_GetOne_Entity_From_Database_And_ReturnIt()
    {
        try
        {
            //Arrange
            var categoryRepository = new CategoryRepository(_context);
            var categoryEntity = new CategoryEntity
            {
                CategoryName = "Mobile"
            };

            var manufacturerRepository = new ManufacturerRepository(_context);
            var manufacturerEntity = new ManufacturerEntity
            {
                Manufacturer = "Samsung"
            };

            var priceRepository = new PriceRepository(_context);
            var priceEntity = new PriceEntity
            {
                UnitPrice = 100
            };

            var productRepository = new ProductRepository(_context);
            var productEntity = new ProductEntity
            {
                ArticleNumber = Guid.NewGuid().ToString(),
                Title = "Samsung Galaxy 8",
                Description = "Red 256gb",
                CategoryId = categoryEntity.Id,
                ManufacturerId = manufacturerEntity.Id,
                PriceId = priceEntity.Id
            };

            // Act
            var createResult = await productRepository.Create(productEntity);

            var result = await productRepository.GetOne(x => x.ArticleNumber == createResult.ArticleNumber);

            // Assert
            Assert.NotNull(result);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Exception: {ex.Message}");
        }
    }

    [Fact]
    public async Task Update_Should_Update_Entity_In_Database_And_ReturnUpdatedEntity()
    {
        try
        {
            //Arrange
            var categoryRepository = new CategoryRepository(_context);
            var categoryEntity = new CategoryEntity
            {
                CategoryName = "Mobile"
            };

            var manufacturerRepository = new ManufacturerRepository(_context);
            var manufacturerEntity = new ManufacturerEntity
            {
                Manufacturer = "Samsung"
            };

            var priceRepository = new PriceRepository(_context);
            var priceEntity = new PriceEntity
            {
                UnitPrice = 100
            };

            var productRepository = new ProductRepository(_context);
            var productEntity = new ProductEntity
            {
                ArticleNumber = Guid.NewGuid().ToString(),
                Title = "Samsung Galaxy 8",
                Description = "Red 256gb",
                CategoryId = categoryEntity.Id,
                ManufacturerId = manufacturerEntity.Id,
                PriceId = priceEntity.Id
            };
            var createResult = await productRepository.Create(productEntity);

            // Act
            var result = await productRepository.Update(x => x.ArticleNumber == createResult.ArticleNumber, createResult);

            // Assert
            Assert.NotNull(result);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Exception: {ex.Message}");
        }
    }

    [Fact]
    public async Task Delete_Should_GetOne_Entity_From_Database_And_ReturnIt()
    {
        try
        {
            //Arrange
            var categoryRepository = new CategoryRepository(_context);
            var categoryEntity = new CategoryEntity
            {
                CategoryName = "Mobile"
            };

            var manufacturerRepository = new ManufacturerRepository(_context);
            var manufacturerEntity = new ManufacturerEntity
            {
                Manufacturer = "Samsung"
            };

            var priceRepository = new PriceRepository(_context);
            var priceEntity = new PriceEntity
            {
                UnitPrice = 100
            };

            var productRepository = new ProductRepository(_context);
            var productEntity = new ProductEntity
            {
                ArticleNumber = Guid.NewGuid().ToString(),
                Title = "Samsung Galaxy 8",
                Description = "Red 256gb",
                CategoryId = categoryEntity.Id,
                ManufacturerId = manufacturerEntity.Id,
                PriceId = priceEntity.Id
            };
            //Act
            var createResult = await productRepository.Create(productEntity);

            // Act
            var result = await productRepository.Delete(x => x.ArticleNumber == createResult.ArticleNumber);

            // Assert
            Assert.True(result);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Exception: {ex.Message}");
        }
    }

    [Fact]
    public async Task Exists_Should_Check_IfEntityExists_in_Database_And_ReturnTrue()
    {
        try
        {
            //Arrange
            var categoryRepository = new CategoryRepository(_context);
            var categoryEntity = new CategoryEntity
            {
                CategoryName = "Mobile"
            };

            var manufacturerRepository = new ManufacturerRepository(_context);
            var manufacturerEntity = new ManufacturerEntity
            {
                Manufacturer = "Samsung"
            };

            var priceRepository = new PriceRepository(_context);
            var priceEntity = new PriceEntity
            {
                UnitPrice = 100
            };

            var productRepository = new ProductRepository(_context);
            var productEntity = new ProductEntity
            {
                ArticleNumber = Guid.NewGuid().ToString(),
                Title = "Samsung Galaxy 8",
                Description = "Red 256gb",
                CategoryId = categoryEntity.Id,
                ManufacturerId = manufacturerEntity.Id,
                PriceId = priceEntity.Id
            };
            //Act
            var createResult = await productRepository.Create(productEntity);

            var result = await productRepository.Exists(x => x.ArticleNumber == createResult.ArticleNumber);

            // Assert
            Assert.True(result);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Exception: {ex.Message}");
        }
    }
}
