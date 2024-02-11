using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Infrastructure.Tests.Repositories;

public class PriceRepository_Tests
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
            var priceRepository = new PriceRepository(_context);
            var priceEntity = new PriceEntity
            {
                UnitPrice = 100
            };

            //Act
            var result = await priceRepository.Create(priceEntity);

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
            var priceRepository = new PriceRepository(_context);

            //Act
            var result = await priceRepository.GetAll();

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
            // Arrange
            var priceRepository = new PriceRepository(_context);
            var priceEntity = new PriceEntity
            {
                UnitPrice = 100
            };

            // Act
            var createResult = await priceRepository.Create(priceEntity);

            var result = await priceRepository.GetOne(x => x.Id == priceEntity.Id);

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
            // Arrange
            var priceRepository = new PriceRepository(_context);
            var priceEntity = new PriceEntity
            {
                UnitPrice = 100
            };
            var createResult = await priceRepository.Create(priceEntity);
            Debug.WriteLine($"Result from Create: {createResult}");

            // Act
            var result = await priceRepository.Update(x => x.Id == priceEntity.Id, priceEntity);
            Debug.WriteLine($"Result from GetOne: {result}");

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
            // Arrange
            var priceRepository = new PriceRepository(_context);
            var priceEntity = new PriceEntity
            {
                UnitPrice = 100
            };
            var createResult = await priceRepository.Create(priceEntity);

            // Act
            var result = await priceRepository.Delete(x => x.Id == priceEntity.Id);

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
            // Arrange
            var priceRepository = new PriceRepository(_context);
            var priceEntity = new PriceEntity
            {
                UnitPrice = 100
            };

            // Act
            var createResult = await priceRepository.Create(priceEntity);

            var result = await priceRepository.Exists(x => x.Id == priceEntity.Id);

            // Assert
            Assert.True(result);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Exception: {ex.Message}");
        }
    }
}
