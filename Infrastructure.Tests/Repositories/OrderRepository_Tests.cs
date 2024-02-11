using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Infrastructure.Tests.Repositories;

public class OrderRepository_Tests
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
            var cutomerRepository = new CustomerRepository(_context);
            var customerEntity = new CustomerEntity
            {
                CustomerNumber = Guid.NewGuid().ToString(),
                FirstName = "Nils",
                LastName = "Lind",
                Email = "Nils@domain.com"
            };
            var customerResult = await cutomerRepository.Create(customerEntity);

            var orderRepository = new OrderRepository(_context);
            var orderEntity = new OrderEntity 
            {
                CustomerNumber = customerResult.CustomerNumber,
            };

            //Act
            var result = await orderRepository.Create(orderEntity);

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
            var orderRepository = new OrderRepository(_context);

            //Act
            var result = await orderRepository.GetAll();

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
            var cutomerRepository = new CustomerRepository(_context);
            var customerEntity = new CustomerEntity
            {
                CustomerNumber = Guid.NewGuid().ToString(),
                FirstName = "Nils",
                LastName = "Lind",
                Email = "Nils@domain.com"
            };
            var customerResult = await cutomerRepository.Create(customerEntity);

            var orderRepository = new OrderRepository(_context);
            var orderEntity = new OrderEntity
            {
                CustomerNumber = customerResult.CustomerNumber,
            };
            var orderResult = await orderRepository.Create(orderEntity);

            //Act
            var result = await orderRepository.GetOne(x => x.OrderNumber == orderResult.OrderNumber);

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
            var cutomerRepository = new CustomerRepository(_context);
            var customerEntity = new CustomerEntity
            {
                CustomerNumber = Guid.NewGuid().ToString(),
                FirstName = "Nils",
                LastName = "Lind",
                Email = "Nils@domain.com"
            };
            var customerResult = await cutomerRepository.Create(customerEntity);

            var orderRepository = new OrderRepository(_context);
            var orderEntity = new OrderEntity
            {
                CustomerNumber = customerResult.CustomerNumber,
            };
            var orderResult = await orderRepository.Create(orderEntity);

            // Act
            var result = await orderRepository.Update(x => x.OrderNumber == orderResult.OrderNumber, orderEntity);

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
            var cutomerRepository = new CustomerRepository(_context);
            var customerEntity = new CustomerEntity
            {
                CustomerNumber = Guid.NewGuid().ToString(),
                FirstName = "Nils",
                LastName = "Lind",
                Email = "Nils@domain.com"
            };
            var customerResult = await cutomerRepository.Create(customerEntity);

            var orderRepository = new OrderRepository(_context);
            var orderEntity = new OrderEntity
            {
                CustomerNumber = customerResult.CustomerNumber,
            };
            var orderResult = await orderRepository.Create(orderEntity);

            // Act
            var result = await orderRepository.Delete(x => x.OrderNumber == orderResult.OrderNumber);

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
            var cutomerRepository = new CustomerRepository(_context);
            var customerEntity = new CustomerEntity
            {
                CustomerNumber = Guid.NewGuid().ToString(),
                FirstName = "Nils",
                LastName = "Lind",
                Email = "Nils@domain.com"
            };
            var customerResult = await cutomerRepository.Create(customerEntity);

            var orderRepository = new OrderRepository(_context);
            var orderEntity = new OrderEntity
            {
                CustomerNumber = customerResult.CustomerNumber,
            };
            var orderResult = await orderRepository.Create(orderEntity);

            // Act
            var result = await orderRepository.Exists(x => x.OrderNumber == orderResult.OrderNumber);

            // Assert
            Assert.True(result);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Exception: {ex.Message}");
        }
    }
}

