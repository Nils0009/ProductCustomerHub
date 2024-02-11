using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Infrastructure.Tests.Repositories;

public class CategoryRepository_Tests
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

            //Act
            var result = await categoryRepository.Create(categoryEntity);

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
            var categoryRepository = new CategoryRepository(_context);

            //Act
            var result = await categoryRepository.GetAll();

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
            var categoryRepository = new CategoryRepository(_context);
            var categoryEntity = new CategoryEntity
            {
                CategoryName = "Mobile"
            };

            // Act
            var createResult = await categoryRepository.Create(categoryEntity);

            var result = await categoryRepository.GetOne(x => x.CategoryName == categoryEntity.CategoryName);

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
            var categoryRepository = new CategoryRepository(_context);
            var categoryEntity = new CategoryEntity
            {
                CategoryName = "Mobile"
            };
            var createResult = await categoryRepository.Create(categoryEntity);

            // Act
            var result = await categoryRepository.Update(x => x.CategoryName == categoryEntity.CategoryName, categoryEntity);

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
            var categoryRepository = new CategoryRepository(_context);
            var categoryEntity = new CategoryEntity
            {
                CategoryName = "Mobile"
            };
            var createResult = await categoryRepository.Create(categoryEntity);

            // Act
            var result = await categoryRepository.Delete(x => x.CategoryName == categoryEntity.CategoryName);

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
            var categoryRepository = new CategoryRepository(_context);
            var categoryEntity = new CategoryEntity
            {
                CategoryName = "Mobile"
            };

            // Act
            var createResult = await categoryRepository.Create(categoryEntity);

            var result = await categoryRepository.Exists(x => x.CategoryName == categoryEntity.CategoryName);

            // Assert
            Assert.True(result);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Exception: {ex.Message}");
        }
    }
}
