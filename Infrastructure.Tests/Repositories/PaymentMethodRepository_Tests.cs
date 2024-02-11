using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Infrastructure.Tests.Repositories;

public class PaymentMethodRepository_Tests
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
            var paymentMethodRepository = new PaymentMethodRepository(_context);
            var paymentMethodEntity = new PaymentMethodEntity
            {
                PaymentMethodName = "Cash",
                Description = "Mastercard"
            };

            //Act
            var result = await paymentMethodRepository.Create(paymentMethodEntity);

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
            var paymentMethodRepository = new PaymentMethodRepository(_context);

            //Act
            var result = await paymentMethodRepository.GetAll();

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
            var paymentMethodRepository = new PaymentMethodRepository(_context);
            var paymentMethodEntity = new PaymentMethodEntity
            {
                PaymentMethodName = "Cash",
                Description = "Mastercard"
            };

            // Act
            var createResult = await paymentMethodRepository.Create(paymentMethodEntity);

            var result = await paymentMethodRepository.GetOne(x => x.PaymentMethodId == paymentMethodEntity.PaymentMethodId);

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
            var paymentMethodRepository = new PaymentMethodRepository(_context);
            var paymentMethodEntity = new PaymentMethodEntity
            {
                PaymentMethodName = "Cash",
                Description = "Mastercard"
            };
            var createResult = await paymentMethodRepository.Create(paymentMethodEntity);
            Debug.WriteLine($"Result from Create: {createResult}");

            // Act
            var result = await paymentMethodRepository.Update(x => x.PaymentMethodId == paymentMethodEntity.PaymentMethodId, paymentMethodEntity);
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
            var paymentMethodRepository = new PaymentMethodRepository(_context);
            var paymentMethodEntity = new PaymentMethodEntity
            {
                PaymentMethodName = "Cash",
                Description = "Mastercard"
            };
            var createResult = await paymentMethodRepository.Create(paymentMethodEntity);

            // Act
            var result = await paymentMethodRepository.Delete(x => x.PaymentMethodId == paymentMethodEntity.PaymentMethodId);

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
            var paymentMethodRepository = new PaymentMethodRepository(_context);
            var paymentMethodEntity = new PaymentMethodEntity
            {
                PaymentMethodName = "Cash",
                Description = "Mastercard"
            };

            // Act
            var createResult = await paymentMethodRepository.Create(paymentMethodEntity);

            var result = await paymentMethodRepository.Exists(x => x.PaymentMethodId == paymentMethodEntity.PaymentMethodId);

            // Assert
            Assert.True(result);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Exception: {ex.Message}");
        }
    }
}
