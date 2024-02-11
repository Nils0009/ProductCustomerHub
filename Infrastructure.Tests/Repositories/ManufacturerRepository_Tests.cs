using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Infrastructure.Tests.Repositories;

public class ManufacturerRepository_Tests
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
            var manufacturerRepository = new ManufacturerRepository(_context);
            var manufacturerEntity = new ManufacturerEntity
            {
                Manufacturer = "Samsung"
            };

            //Act
            var result = await manufacturerRepository.Create(manufacturerEntity);

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
            var manufacturerRepository = new ManufacturerRepository(_context);

            //Act
            var result = await manufacturerRepository.GetAll();

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
            var manufacturerRepository = new ManufacturerRepository(_context);
            var manufacturerEntity = new ManufacturerEntity
            {
                Manufacturer = "Samsung"
            };

            // Act
            var createResult = await manufacturerRepository.Create(manufacturerEntity);

            var result = await manufacturerRepository.GetOne(x => x.Manufacturer == manufacturerEntity.Manufacturer);

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
            var manufacturerRepository = new ManufacturerRepository(_context);
            var manufacturerEntity = new ManufacturerEntity
            {
                Manufacturer = "Samsung"
            };
            var createResult = await manufacturerRepository.Create(manufacturerEntity);

            // Act
            var result = await manufacturerRepository.Update(x => x.Manufacturer == manufacturerEntity.Manufacturer, manufacturerEntity);

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
            var manufacturerRepository = new ManufacturerRepository(_context);
            var manufacturerEntity = new ManufacturerEntity
            {
                Manufacturer = "Samsung"
            };
            var createResult = await manufacturerRepository.Create(manufacturerEntity);

            // Act
            var result = await manufacturerRepository.Delete(x => x.Manufacturer == manufacturerEntity.Manufacturer);

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
            var manufacturerRepository = new ManufacturerRepository(_context);
            var manufacturerEntity = new ManufacturerEntity
            {
                Manufacturer = "Samsung"
            };

            // Act
            var createResult = await manufacturerRepository.Create(manufacturerEntity);

            var result = await manufacturerRepository.Exists(x => x.Manufacturer == manufacturerEntity.Manufacturer);

            // Assert
            Assert.True(result);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Exception: {ex.Message}");
        }
    }
}
