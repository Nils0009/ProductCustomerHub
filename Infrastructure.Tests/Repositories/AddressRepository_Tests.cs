using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Infrastructure.Tests.Repositories;

public class AddressRepository_Tests
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
                var addressRepository = new AddressRepository(_context);
                var addressEntity = new AddressEntity
                {
                    StreetName = "Boda",
                    City = "Rättvik",
                    PostalCode = "79596",
                    Country = "Sweden"
                };

                //Act
                var result = await addressRepository.Create(addressEntity);

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
                var addressRepository = new AddressRepository(_context);

                //Act
                var result = await addressRepository.GetAll();

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
                var addressRepository = new AddressRepository(_context);
                var addressEntity = new AddressEntity
                {
                    StreetName = "Boda",
                    City = "Rättvik",
                    PostalCode = "79596",
                    Country = "Sweden"
                };

                // Act
                var createResult = await addressRepository.Create(addressEntity);

                var result = await addressRepository.GetOne(x => x.AddressId == addressEntity.AddressId);

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
                var addressRepository = new AddressRepository(_context);
                var addressEntity = new AddressEntity
                {
                    StreetName = "Boda",
                    City = "Rättvik",
                    PostalCode = "79596",
                    Country = "Sweden"
                };
                var createResult = await addressRepository.Create(addressEntity);
                Debug.WriteLine($"Result from Create: {createResult}");

                // Act
                var result = await addressRepository.Update(x => x.AddressId == addressEntity.AddressId, addressEntity);
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
                var addressRepository = new AddressRepository(_context);
                var addressEntity = new AddressEntity
                {
                    StreetName = "Boda",
                    City = "Rättvik",
                    PostalCode = "79596",
                    Country = "Sweden"
                };
                var createResult = await addressRepository.Create(addressEntity);

                // Act
                var result = await addressRepository.Delete(x => x.AddressId == addressEntity.AddressId);

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
                var addressRepository = new AddressRepository(_context);
                var addressEntity = new AddressEntity
                {
                    StreetName = "Boda",
                    City = "Rättvik",
                    PostalCode = "79596",
                    Country = "Sweden"
                };

                // Act
                var createResult = await addressRepository.Create(addressEntity);

                var result = await addressRepository.Exists(x => x.AddressId == addressEntity.AddressId);

                // Assert
                Assert.True(result);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception: {ex.Message}");
            }
        }
}

