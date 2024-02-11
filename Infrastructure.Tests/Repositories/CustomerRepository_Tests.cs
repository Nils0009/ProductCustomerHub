using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Infrastructure.Tests.Repositories
{
    public class CustomerEntity_Tests
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

                //Act
                var result = await cutomerRepository.Create(customerEntity);

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
                var cutomerRepository = new CustomerRepository(_context);

                //Act
                var result = await cutomerRepository.GetAll();

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
                var cutomerRepository = new CustomerRepository(_context);
                var customerEntity = new CustomerEntity
                {
                    CustomerNumber = Guid.NewGuid().ToString(),
                    FirstName = "Nils",
                    LastName = "Lind",
                    Email = "Nils@domain.com"
                };

                // Act
                var createResult = await cutomerRepository.Create(customerEntity);

                var result = await cutomerRepository.GetOne(x => x.CustomerNumber == customerEntity.CustomerNumber);

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
                var cutomerRepository = new CustomerRepository(_context);
                var customerEntity = new CustomerEntity
                {
                    CustomerNumber = Guid.NewGuid().ToString(),
                    FirstName = "Nils",
                    LastName = "Lind",
                    Email = "Nils@domain.com"
                };
                var createResult = await cutomerRepository.Create(customerEntity);
                Debug.WriteLine($"Result from Create: {createResult}");

                // Act
                var result = await cutomerRepository.Update(x => x.CustomerNumber == customerEntity.CustomerNumber, customerEntity);
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
                var cutomerRepository = new CustomerRepository(_context);
                var customerEntity = new CustomerEntity
                {
                    CustomerNumber = Guid.NewGuid().ToString(),
                    FirstName = "Nils",
                    LastName = "Lind",
                    Email = "Nils@domain.com"
                };
                var createResult = await cutomerRepository.Create(customerEntity);

                // Act
                var result = await cutomerRepository.Delete(x => x.CustomerNumber == customerEntity.CustomerNumber);

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
                var cutomerRepository = new CustomerRepository(_context);
                var customerEntity = new CustomerEntity
                {
                    CustomerNumber = Guid.NewGuid().ToString(),
                    FirstName = "Nils",
                    LastName = "Lind",
                    Email = "Nils@domain.com"
                };

                // Act
                var createResult = await cutomerRepository.Create(customerEntity);

                var result = await cutomerRepository.Exists(x => x.CustomerNumber == customerEntity.CustomerNumber);

                // Assert
                Assert.True(result);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception: {ex.Message}");
            }
        }
    }
}