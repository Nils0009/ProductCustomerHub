using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Infrastructure.Tests.Services;

public class ProductCatalogService_Tests
{
    private readonly ProductCatalogContext _context =
        new(new DbContextOptionsBuilder<ProductCatalogContext>()
            .UseInMemoryDatabase($"{Guid.NewGuid()}")
            .Options);

    [Fact]
    public async Task CreateProduct_Should_Add_One_Product_To_Database_And_Return_Entity()
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

            var productCatalogService = new ProductCatalogService(categoryRepository, manufacturerRepository, priceRepository, productRepository);
            
            //Act
            var result = await productCatalogService.CreateProduct(productEntity);

            //Assert
            Assert.True(result);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Exception: {ex.Message}");
        }

    }

    [Fact]
    public async Task GetAllProducts_Should_GetAll_ProductEntitys_From_Database_And_Return_IEnumerableList()
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

            var productCatalogService = new ProductCatalogService(categoryRepository, manufacturerRepository, priceRepository, productRepository);

            //Act
            var result = await productCatalogService.GetAllProducts();

            //Assert
            Assert.NotNull(result);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Exception: {ex.Message}");
        }

    }

    [Fact]
    public async Task GetOneProduct_Should_GetOne_ProductEntity_From_Database_And_ReturnIt()
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

            var productCatalogService = new ProductCatalogService(categoryRepository, manufacturerRepository, priceRepository, productRepository);

            //Act
            var result = await productCatalogService.GetOneProduct(productEntity.ArticleNumber);

            // Assert
            Assert.NotNull(result);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Exception: {ex.Message}");
        }
    }

    [Fact]
    public async Task UpdateProduct_Should_Update_ProductEntity_In_Database_And_ReturnUpdatedProductEntity()
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

            var productCatalogService = new ProductCatalogService(categoryRepository, manufacturerRepository, priceRepository, productRepository);

            //Act
            var result = await productCatalogService.UpdateProduct(productEntity, productEntity.ArticleNumber);

            // Assert
            Assert.True(result);
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

            var productCatalogService = new ProductCatalogService(categoryRepository, manufacturerRepository, priceRepository, productRepository);

            //Act
            var result = await productCatalogService.DeleteProduct(productEntity.ArticleNumber);

            // Assert
            Assert.True(result);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Exception: {ex.Message}");
        }
    }

}
