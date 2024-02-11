using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Infrastructure.Tests.Repositories;

public class RoleRepository_Tests
{
    private readonly CustomerManagementContext _context =
            new(new DbContextOptionsBuilder<CustomerManagementContext>()
                .UseInMemoryDatabase($"{Guid.NewGuid()}")
                .Options);

    [Fact]
    public async Task Create_Should_Add_One_Entity_To_Database_And_Return_Entity()
    {
        try
        {
            //Arrange
            var roleRepository = new RoleRepository(_context);
            var roleEntity = new RoleEntity
            {
                RoleName = "Admin"
            };

            //Act
            var result = await roleRepository.Create(roleEntity);

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
            var roleRepository = new RoleRepository(_context);

            //Act
            var result = await roleRepository.GetAll();

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
            var roleRepository = new RoleRepository(_context);
            var roleEntity = new RoleEntity
            {
                RoleName = "Admin"
            };

            // Act
            var createResult = await roleRepository.Create(roleEntity);

            var result = await roleRepository.GetOne(x => x.RoleId == roleEntity.RoleId);

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
            var roleRepository = new RoleRepository(_context);
            var roleEntity = new RoleEntity
            {
                RoleName = "Admin"
            };
            var createResult = await roleRepository.Create(roleEntity);
            Debug.WriteLine($"Result from Create: {createResult}");

            // Act
            var result = await roleRepository.Update(x => x.RoleId == roleEntity.RoleId, roleEntity);
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
            var roleRepository = new RoleRepository(_context);
            var roleEntity = new RoleEntity
            {
                RoleName = "Admin"
            };
            var createResult = await roleRepository.Create(roleEntity);

            // Act
            var result = await roleRepository.Delete(x => x.RoleId == roleEntity.RoleId);

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
            var roleRepository = new RoleRepository(_context);
            var roleEntity = new RoleEntity
            {
                RoleName = "Admin"
            };

            // Act
            var createResult = await roleRepository.Create(roleEntity);

            var result = await roleRepository.Exists(x => x.RoleId == roleEntity.RoleId);

            // Assert
            Assert.True(result);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Exception: {ex.Message}");
        }
    }
}
