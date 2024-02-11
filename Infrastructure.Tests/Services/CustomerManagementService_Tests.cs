using Infrastructure.Contexts;
using Infrastructure.Dtos;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Infrastructure.Tests.Services;

public class CustomerManagementService_Tests
{
    private readonly CustomerManagementContext _context =
           new(new DbContextOptionsBuilder<CustomerManagementContext>()
               .UseInMemoryDatabase($"{Guid.NewGuid()}")
               .Options);

    [Fact]
    public async Task CreateCustomer_Should_Add_One_CustomerEntity_To_Database_And_Return_True()
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

            var roleRepository = new RoleRepository(_context);
            var roleEntity = new RoleEntity
            {
                RoleName = "Admin"
            };
            var roleResult = await roleRepository.Create(roleEntity);

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

            var customerRegistrationDto = new CustomerRegistrationDto
            {
                FirstName = "Nils",
                LastName = "Lind",
                Email = "Nils@domain.com",
                RoleName = "Admin",
                StreetName = "Boda",
                City = "Rättvik",
                PostalCode = "79596",
                Country = "Sweden"
            };

            var customerManagementService = new CustomerManagementService(
                addressRepository,
                cutomerRepository,
                roleRepository,
                customerAddressRepository
            );

            //Act
            var result = await customerManagementService.CreateCustomer(customerRegistrationDto);

            //Assert
            Assert.True(result);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Exception: {ex.Message}");
        }

    }

    [Fact]
    public async Task GetAllCustomers_Should_Return_List_Of_CustomerDtos()
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

            var roleRepository = new RoleRepository(_context);
            var roleEntity = new RoleEntity
            {
                RoleName = "Admin"
            };
            var roleResult = await roleRepository.Create(roleEntity);
        

            var customerDto = new CustomerDto
            {
                FirstName = customerResult.FirstName,
                LastName = customerResult.LastName,
                Email = customerResult.Email,
                RoleName = roleResult.RoleName,
            };

            var customerManagementService = new CustomerManagementService(cutomerRepository, roleRepository);

            var customerEntities = new List<CustomerDto>{customerDto};

            //Act
            var result = await customerManagementService.GetAllCustomers();

            //Assert
            Assert.NotNull(result);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Exception: {ex.Message}");
        }

    }

    [Fact]
    public async Task GetOneCustomer_Should_Return_CustomerRegistrationDto_IfExists()
    {
        try
        {
            // Arrange
            var cutomerRepository = new CustomerRepository(_context);
            var customerEntity = new CustomerEntity
            {
                CustomerNumber = Guid.NewGuid().ToString(),
                FirstName = "Nils",
                LastName = "Lind",
                Email = "Nils@domain.com"
            };
            var customerResult = await cutomerRepository.Create(customerEntity);

            var roleRepository = new RoleRepository(_context);
            var roleEntity = new RoleEntity
            {
                RoleName = "Admin"
            };
            var roleResult = await roleRepository.Create(roleEntity);

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
            var customerManagementService = new CustomerManagementService(
                addressRepository,
                cutomerRepository,
                roleRepository,
                customerAddressRepository
            );

            //Act
            var result = await customerManagementService.GetOneCustomer(customerResult.Email);


            // Assert
            Assert.NotNull(result);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Exception: {ex.Message}");
        }
    }

    [Fact]
    public async Task GetCustomerWithCustomerNumber_Should_Return_CustomerRegistrationDto_IfExists()
    {
        try
        {
            // Arrange
            var cutomerRepository = new CustomerRepository(_context);
            var customerEntity = new CustomerEntity
            {
                CustomerNumber = Guid.NewGuid().ToString(),
                FirstName = "Nils",
                LastName = "Lind",
                Email = "Nils@domain.com"
            };
            var customerResult = await cutomerRepository.Create(customerEntity);

            var roleRepository = new RoleRepository(_context);
            var roleEntity = new RoleEntity
            {
                RoleName = "Admin"
            };
            var roleResult = await roleRepository.Create(roleEntity);

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
            var customerManagementService = new CustomerManagementService(
                addressRepository,
                cutomerRepository,
                roleRepository,
                customerAddressRepository
            );

            //Act
            var result = await customerManagementService.GetCustomerWithCustomerNumber(customerResult.CustomerNumber);


            // Assert
            Assert.NotNull(result);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Exception: {ex.Message}");
        }
    }

    [Fact]
    public async Task UpdateCustomer_Should_Update_Customer_And_Return_True()
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

            var roleRepository = new RoleRepository(_context);
            var roleEntity = new RoleEntity
            {
                RoleName = "Admin"
            };
            var roleResult = await roleRepository.Create(roleEntity);

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

            var customerRegistrationDto = new CustomerRegistrationDto
            {
                FirstName = "Nils",
                LastName = "Lind",
                Email = "Nils@domain.com",
                RoleName = "Admin",
                StreetName = "Boda",
                City = "Rättvik",
                PostalCode = "79596",
                Country = "Sweden"
            };

            var customerManagementService = new CustomerManagementService(
                addressRepository,
                cutomerRepository,
                roleRepository,
                customerAddressRepository
            );

            //Act
            var result = await customerManagementService.UpdateCustomer(customerRegistrationDto, customerResult.Email);

            // Assert
            Assert.True(result);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Exception: {ex.Message}");
        }
    }

    [Fact]
    public async Task DeleteCustomer_Should_Delete_Customer_And_Return_True()
    {
        try
        {
            //Arrange
            var cutomerRepository = new CustomerRepository(_context);

            var roleRepository = new RoleRepository(_context);
            var roleEntity = new RoleEntity
            {
                RoleName = "Admin"
            };
            var roleResult = await roleRepository.Create(roleEntity);

            var customerEntity = new CustomerEntity
            {
                CustomerNumber = Guid.NewGuid().ToString(),
                FirstName = "Nils",
                LastName = "Lind",
                Email = "Nils@domain.com",
                Role = roleResult,
            };
            var customerResult = await cutomerRepository.Create(customerEntity);
            var customerManagementService = new CustomerManagementService(cutomerRepository, roleRepository);

            //Act
            var result = await customerManagementService.DeleteCustomer(customerResult.Email);

            // Assert
            Assert.True(result);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Exception: {ex.Message}");
        }
    }

}
