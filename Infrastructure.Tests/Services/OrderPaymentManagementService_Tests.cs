using Infrastructure.Contexts;
using Infrastructure.Dtos;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Infrastructure.Tests.Services;

public class OrderPaymentManagementService_Tests
{
    private readonly CustomerManagementContext _context =
       new(new DbContextOptionsBuilder<CustomerManagementContext>()
           .UseInMemoryDatabase($"{Guid.NewGuid()}")
           .Options);

    [Fact]
    public async Task CreateOrder_Should_Add_One_OrderEntity_To_Database_And_Return_True()
    {
        try
        {
            //Arrange
            var roleRepository = new RoleRepository(_context);
            var roleEntity = new RoleEntity
            {
                RoleName = "Admin"
            };
            var roleResult = await roleRepository.Create(roleEntity);

            var cutomerRepository = new CustomerRepository(_context);
            var customerEntity = new CustomerEntity
            {
                CustomerNumber = Guid.NewGuid().ToString(),
                FirstName = "Nils",
                LastName = "Lind",
                Email = "Nils@domain.com",
                Role = roleEntity,
            };
            var customerResult = await cutomerRepository.Create(customerEntity);


            var orderRepository = new OrderRepository(_context);
            var orderEntity = new OrderEntity
            {
                Customer = customerResult,
                CustomerNumber = customerResult.CustomerNumber
            };
            var orderResult = await orderRepository.Create(orderEntity);

            var paymentMethodRepository = new PaymentMethodRepository(_context);
            var paymentMethodEntity = new PaymentMethodEntity
            {
                PaymentMethodName = "Cash",
                Description = "Mastercard"
            };
            var paymentMethodResult = await paymentMethodRepository.Create(paymentMethodEntity);


            var orderRegistrationDto = new OrderRegistrationDto
            {
                CustomerNumber = customerResult.CustomerNumber,
                FirstName = "Nils",
                LastName = "Lind",
                Email = "Nils@domain.com",
                PaymentMethodName = paymentMethodResult.PaymentMethodName,
                Description = paymentMethodResult.Description
            };

            var orderPaymentManagementService = new OrderPaymentManagementService(
                orderRepository,
                paymentMethodRepository,
                cutomerRepository
                );

            //Act
            var result = await orderPaymentManagementService.CreateOrder(orderRegistrationDto);

            //Assert
            Assert.True(result);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Exception: {ex.Message}");
        }

    }

    [Fact]
    public async Task GetAllOrders_Should_Return_List_Of_OrderEntity()
    {
        try
        {
            //Arrange
            var roleRepository = new RoleRepository(_context);
            var roleEntity = new RoleEntity
            {
                RoleName = "Admin"
            };
            var roleResult = await roleRepository.Create(roleEntity);

            var cutomerRepository = new CustomerRepository(_context);
            var customerEntity = new CustomerEntity
            {
                CustomerNumber = Guid.NewGuid().ToString(),
                FirstName = "Nils",
                LastName = "Lind",
                Email = "Nils@domain.com",
                Role = roleEntity,
            };
            var customerResult = await cutomerRepository.Create(customerEntity);


            var orderRepository = new OrderRepository(_context);
            var orderEntity = new OrderEntity
            {
                Customer = customerResult,
                CustomerNumber = customerResult.CustomerNumber
            };
            var orderResult = await orderRepository.Create(orderEntity);

            var paymentMethodRepository = new PaymentMethodRepository(_context);
            var paymentMethodEntity = new PaymentMethodEntity
            {
                PaymentMethodName = "Cash",
                Description = "Mastercard"
            };
            var paymentMethodResult = await paymentMethodRepository.Create(paymentMethodEntity);


            var orderPaymentManagementService = new OrderPaymentManagementService(
                orderRepository,
                paymentMethodRepository,
                cutomerRepository
                );

            //Act
            var result = await orderPaymentManagementService.GetAllOrders();

            //Assert
            Assert.NotNull(result);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Exception: {ex.Message}");
        }

    }

    [Fact]
    public async Task GetOneOrders_Should_Return_OrderDto_IfExists()
    {
        try
        {   
            //Arrange
            var roleRepository = new RoleRepository(_context);
            var roleEntity = new RoleEntity
            {
                RoleName = "Admin"
            };
            var roleResult = await roleRepository.Create(roleEntity);

            var cutomerRepository = new CustomerRepository(_context);
            var customerEntity = new CustomerEntity
            {
                CustomerNumber = Guid.NewGuid().ToString(),
                FirstName = "Nils",
                LastName = "Lind",
                Email = "Nils@domain.com",
                Role = roleEntity,
            };
            var customerResult = await cutomerRepository.Create(customerEntity);


            var orderRepository = new OrderRepository(_context);
            var orderEntity = new OrderEntity
            {
                Customer = customerResult,
                CustomerNumber = customerResult.CustomerNumber
            };
            var orderResult = await orderRepository.Create(orderEntity);

            var paymentMethodRepository = new PaymentMethodRepository(_context);
            var paymentMethodEntity = new PaymentMethodEntity
            {
                PaymentMethodName = "Cash",
                Description = "Mastercard"
            };
            var paymentMethodResult = await paymentMethodRepository.Create(paymentMethodEntity);


            var orderPaymentManagementService = new OrderPaymentManagementService(
                orderRepository,
                paymentMethodRepository,
                cutomerRepository
                );

            //Act
            var result = await orderPaymentManagementService.GetOneOrder(orderResult.OrderNumber);


            // Assert
            Assert.NotNull(result);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Exception: {ex.Message}");
        }
    }

    [Fact]
    public async Task UpdateOrder_Should_Update_Order_And_Return_True()
    {
        try
        {
            //Arrange
            var roleRepository = new RoleRepository(_context);
            var roleEntity = new RoleEntity
            {
                RoleName = "Admin"
            };
            var roleResult = await roleRepository.Create(roleEntity);


            var cutomerRepository = new CustomerRepository(_context);
            var customerEntity = new CustomerEntity
            {
                CustomerNumber = Guid.NewGuid().ToString(),
                FirstName = "Nils",
                LastName = "Lind",
                Email = "Nils@domain.com",
                Role = roleResult
            };
            var customerResult = await cutomerRepository.Create(customerEntity);


            var addressRepository = new AddressRepository(_context);
            var addressEntity = new AddressEntity
            {
                StreetName = "Boda",
                City = "Rättvik",
                PostalCode = "79596",
                Country = "Sweden"
            };
            var addressResult = await addressRepository.Create(addressEntity);


            var customerAddressRepository = new CustomerAddressRepository(_context);
            var customerAddressEntity = new CustomerAddressEntity
            {
                CustomerNumber = customerResult.CustomerNumber,
                AddressId = addressResult.AddressId
            };
            var customerAddressResult = await customerAddressRepository.Create(customerAddressEntity);


            var orderRepository = new OrderRepository(_context);
            var orderEntity = new OrderEntity
            {
                Customer = customerResult,
                CustomerNumber = customerResult.CustomerNumber
            };
            var orderResult = await orderRepository.Create(orderEntity);


            var paymentMethodRepository = new PaymentMethodRepository(_context);
            var paymentMethodEntity = new PaymentMethodEntity
            {
                PaymentMethodName = "Cash",
                Description = "Mastercard"
            };
            var paymentMethodResult = await paymentMethodRepository.Create(paymentMethodEntity);


            var orderDto = new OrderDto
            {
                OrderNumber = orderResult.OrderNumber,
                Description = paymentMethodResult.Description,
                CustomerNumber = customerResult.CustomerNumber,
                FirstName = customerResult.FirstName,
                LastName = customerResult.LastName,
                Email = customerResult.Email,
                PaymentMethodName = paymentMethodResult.PaymentMethodName,
                StreetName = addressResult.StreetName,
                City = addressResult.City,
                PostalCode = addressResult.PostalCode,
                Country = addressResult.Country
            };

            var orderPaymentManagementService = new OrderPaymentManagementService(
                orderRepository,
                paymentMethodRepository,
                cutomerRepository
                );

            //Act
            var result = await orderPaymentManagementService.UpdateOrder(orderDto, orderResult.OrderNumber);

            // Assert
            Assert.True(result);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Exception: {ex.Message}");
        }
    }

    [Fact]
    public async Task DeleteOrder_Should_Delete_Order_And_Return_True()
    {
        try
        {
            //Arrange
            var roleRepository = new RoleRepository(_context);
            var roleEntity = new RoleEntity
            {
                RoleName = "Admin"
            };
            var roleResult = await roleRepository.Create(roleEntity);

            var cutomerRepository = new CustomerRepository(_context);
            var customerEntity = new CustomerEntity
            {
                CustomerNumber = Guid.NewGuid().ToString(),
                FirstName = "Nils",
                LastName = "Lind",
                Email = "Nils@domain.com",
                Role = roleEntity,
            };
            var customerResult = await cutomerRepository.Create(customerEntity);


            var orderRepository = new OrderRepository(_context);
            var orderEntity = new OrderEntity
            {
                Customer = customerResult,
                CustomerNumber = customerResult.CustomerNumber
            };
            var orderResult = await orderRepository.Create(orderEntity);

            var paymentMethodRepository = new PaymentMethodRepository(_context);
            var paymentMethodEntity = new PaymentMethodEntity
            {
                PaymentMethodName = "Cash",
                Description = "Mastercard"
            };
            var paymentMethodResult = await paymentMethodRepository.Create(paymentMethodEntity);


            var orderPaymentManagementService = new OrderPaymentManagementService(
                orderRepository,
                paymentMethodRepository,
                cutomerRepository
                );

            //Act
            var result = await orderPaymentManagementService.DeleteOrder(orderResult.OrderNumber);

            // Assert
            Assert.True(result);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Exception: {ex.Message}");
        }
    }
}
