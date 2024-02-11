using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Infrastructure.Tests.Repositories;

public class CustomerAddressRepository_Tests
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

            var addressRepository = new AddressRepository(_context);
            var addressEntity = new AddressEntity
            {
                StreetName = "Boda",
                City = "Rättvik",
                PostalCode = "79596",
                Country = "Sweden"
            };

            var addressResult = await addressRepository.Create(addressEntity);
            var customerResult = await cutomerRepository.Create(customerEntity);

            var customerAddressRepository = new CustomerAddressRepository(_context);
            var customerAddressEntity = new CustomerAddressEntity
            {
                CustomerNumber = customerResult.CustomerNumber,
                AddressId = addressResult.AddressId
            };

            //Act
            var result = await customerAddressRepository.Create(customerAddressEntity);

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
            var customerAddressRepository = new CustomerAddressRepository(_context);

            //Act
            var result = await customerAddressRepository.GetAll();

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

            var addressRepository = new AddressRepository(_context);
            var addressEntity = new AddressEntity
            {
                StreetName = "Boda",
                City = "Rättvik",
                PostalCode = "79596",
                Country = "Sweden"
            };

            var addressResult = await addressRepository.Create(addressEntity);
            var customerResult = await cutomerRepository.Create(customerEntity);

            var customerAddressRepository = new CustomerAddressRepository(_context);
            var customerAddressEntity = new CustomerAddressEntity
            {
                CustomerNumber = customerResult.CustomerNumber,
                AddressId = addressResult.AddressId
            };

            //Act
            var result = await customerAddressRepository
                .GetOne(x => x.Customer.CustomerNumber == customerAddressEntity.CustomerNumber && customerAddressEntity.AddressId == addressEntity.AddressId);

            //Assert
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

            var addressRepository = new AddressRepository(_context);
            var addressEntity = new AddressEntity
            {
                StreetName = "Boda",
                City = "Rättvik",
                PostalCode = "79596",
                Country = "Sweden"
            };

            var addressResult = await addressRepository.Create(addressEntity);
            var customerResult = await cutomerRepository.Create(customerEntity);

            var customerAddressRepository = new CustomerAddressRepository(_context);
            var customerAddressEntity = new CustomerAddressEntity
            {
                CustomerNumber = customerResult.CustomerNumber,
                AddressId = addressResult.AddressId
            };

            // Act
            var result = await customerAddressRepository
                .Update(x => x.AddressId == addressEntity.AddressId && customerEntity.CustomerNumber == customerAddressEntity.CustomerNumber, customerAddressEntity);

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
        { //Arrange
            var cutomerRepository = new CustomerRepository(_context);
            var customerEntity = new CustomerEntity
            {
                CustomerNumber = Guid.NewGuid().ToString(),
                FirstName = "Nils",
                LastName = "Lind",
                Email = "Nils@domain.com"
            };

            var addressRepository = new AddressRepository(_context);
            var addressEntity = new AddressEntity
            {
                StreetName = "Boda",
                City = "Rättvik",
                PostalCode = "79596",
                Country = "Sweden"
            };

            var addressResult = await addressRepository.Create(addressEntity);
            var customerResult = await cutomerRepository.Create(customerEntity);

            var customerAddressRepository = new CustomerAddressRepository(_context);
            var customerAddressEntity = new CustomerAddressEntity
            {
                CustomerNumber = customerResult.CustomerNumber,
                AddressId = addressResult.AddressId
            };

            // Act
            var result = await customerAddressRepository
                .Delete(x => x.AddressId == addressEntity.AddressId && customerEntity.CustomerNumber == customerAddressEntity.CustomerNumber);

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

            var addressRepository = new AddressRepository(_context);
            var addressEntity = new AddressEntity
            {
                StreetName = "Boda",
                City = "Rättvik",
                PostalCode = "79596",
                Country = "Sweden"
            };

            var addressResult = await addressRepository.Create(addressEntity);
            var customerResult = await cutomerRepository.Create(customerEntity);

            var customerAddressRepository = new CustomerAddressRepository(_context);
            var customerAddressEntity = new CustomerAddressEntity
            {
                CustomerNumber = customerResult.CustomerNumber,
                AddressId = addressResult.AddressId
            };


            // Act
            var result = await customerAddressRepository
                .Exists(x => x.AddressId == addressEntity.AddressId && customerEntity.CustomerNumber == customerAddressEntity.CustomerNumber);

            // Assert
            Assert.True(result);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Exception: {ex.Message}");
        }
    }
}

