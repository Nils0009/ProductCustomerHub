using Infrastructure.Dtos;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using System.Diagnostics;


namespace Infrastructure.Services
{
    public class CustomerManagementService
    {
        private readonly AddressRepository _addressRepository;
        private readonly CustomerRepository _customerRepository;
        private readonly RoleRepository _roleRepository;
        private readonly CustomerAddressRepository _customerAddressRepository;

        public CustomerManagementService(CustomerRepository customerRepository, RoleRepository roleRepository)
        {
            _customerRepository = customerRepository;
            _roleRepository = roleRepository;
            _addressRepository = null!;
            _customerAddressRepository = null!;
        }

        public CustomerManagementService(
            AddressRepository addressRepository,
            CustomerRepository customerRepository,
            RoleRepository roleRepository,
            CustomerAddressRepository customerAddressRepository)
        {
            _addressRepository = addressRepository;
            _customerRepository = customerRepository;
            _roleRepository = roleRepository;
            _customerAddressRepository = customerAddressRepository;
        }

        public async Task<bool> CreateCustomer(CustomerRegistrationDto customer)
        {
            try
            {
                var existingCustomer = await _customerRepository.GetOne(x => x.Email == customer.Email);

                if (existingCustomer == null)
                {
                    var role = await _roleRepository.GetOne(x => x.RoleName == customer.RoleName);
                    role ??= await _roleRepository.Create(new RoleEntity
                    {
                        RoleName = customer.RoleName,
                    });

                    var customerAddress = await _addressRepository.GetOne(x =>
                        x.StreetName == customer.StreetName &&
                        x.City == customer.City &&
                        x.PostalCode == customer.PostalCode &&
                        x.Country == customer.Country);

                    if (customerAddress == null)
                    {
                        customerAddress = await _addressRepository.Create(new AddressEntity
                        {
                            StreetName = customer.StreetName,
                            City = customer.City,
                            PostalCode = customer.PostalCode,
                            Country = customer.Country
                        });
                    }

                    var newCustomer = new CustomerEntity
                    {
                        CustomerNumber = Guid.NewGuid().ToString(),
                        FirstName = customer.FirstName,
                        LastName = customer.LastName,
                        Email = customer.Email,
                        RoleId = role.RoleId,
                    };

                    var createdCustomer = await _customerRepository.Create(newCustomer);

                    if (createdCustomer != null)
                    {
                        var customerAddressEntity = new CustomerAddressEntity
                        {
                            CustomerNumber = createdCustomer.CustomerNumber,
                            AddressId = customerAddress.AddressId
                        };

                        await _customerAddressRepository.Create(customerAddressEntity);

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return false;
        }

        public async Task<IEnumerable<CustomerDto>> GetAllCustomers()
        {
            try
            {
                var customers = new List<CustomerDto>();
                var result = await _customerRepository.GetAll();
                if (result != null)
                {
                    foreach (var customer in result)
                    {
                        var customerDto = new CustomerDto
                        {
                            CustomerNumber = customer.CustomerNumber,
                            FirstName = customer.FirstName,
                            LastName = customer.LastName,
                            Email = customer.Email,
                        };

                        var role = await _roleRepository.GetOne(x => x.RoleId == customer.RoleId);
                        if (role != null)
                        {
                            customerDto.RoleName = role.RoleName;
                        }
                        customers.Add(customerDto);
                    }

                    return customers;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return null!;
        }

        public async Task<CustomerRegistrationDto> GetOneCustomer(string email)
        {
            try
            {
                var customer = new CustomerRegistrationDto();
                var existingCustomer = await _customerRepository.GetOne(x => x.Email == email);

                if (existingCustomer != null)
                {
                    customer.FirstName = existingCustomer.FirstName;
                    customer.LastName = existingCustomer.LastName;
                    customer.Email = existingCustomer.Email;
                    customer.RoleName = existingCustomer.Role.RoleName;

                    var firstAddress = existingCustomer.CustomerAddresses.FirstOrDefault();

                    if (firstAddress != null)
                    {
                        customer.StreetName = firstAddress.Address.StreetName;
                        customer.City = firstAddress.Address.City;
                        customer.PostalCode = firstAddress.Address.PostalCode;
                        customer.Country = firstAddress.Address.Country;
                    }

                    return customer;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return null!;
        }

        public async Task<CustomerDto> GetCustomerWithCustomerNumber(string customerNumber)
        {
            try
            {
                var customer = new CustomerDto();
                var existingCustomer = await _customerRepository.GetOne(x => x.CustomerNumber == customerNumber);

                if (existingCustomer != null)
                {
                    customer.CustomerNumber = existingCustomer.CustomerNumber;
                    customer.FirstName = existingCustomer.FirstName;
                    customer.LastName = existingCustomer.LastName;
                    customer.Email = existingCustomer.Email;
                    customer.RoleName = existingCustomer.Role.RoleName;

                    return customer;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return null!;
        }

        public async Task<bool> UpdateCustomer(CustomerRegistrationDto customer, string email)
        {
            try
            {
                var existingCustomer = await _customerRepository.GetOne(x => x.Email == email);

                if (existingCustomer != null)
                {
                    existingCustomer.FirstName = customer.FirstName;
                    existingCustomer.LastName = customer.LastName;
                    existingCustomer.Email = customer.Email;

                    var role = await _roleRepository.GetOne(x => x.RoleName == customer.RoleName);
                    role ??= await _roleRepository.Create(new RoleEntity
                    {
                        RoleName = customer.RoleName,
                    });

                    existingCustomer.RoleId = role.RoleId;

                    var customerAddress = await _addressRepository.GetOne(x =>
                        x.StreetName == customer.StreetName &&
                        x.City == customer.City &&
                        x.PostalCode == customer.PostalCode &&
                        x.Country == customer.Country);

                    if (customerAddress == null)
                    {
                        customerAddress = await _addressRepository.Create(new AddressEntity
                        {
                            StreetName = customer.StreetName,
                            City = customer.City,
                            PostalCode = customer.PostalCode,
                            Country = customer.Country
                        });
                    }

                    existingCustomer.CustomerAddresses.Clear();

                    existingCustomer.CustomerAddresses.Add(new CustomerAddressEntity
                    {
                        CustomerNumber = existingCustomer.CustomerNumber,
                        AddressId = customerAddress.AddressId
                    });

                    await _roleRepository.Update(x => x.RoleId == role.RoleId, role);
                    await _addressRepository.Update(x => x.AddressId == customerAddress!.AddressId, customerAddress!);
                    await _customerRepository.Update(x => x.CustomerNumber == existingCustomer.CustomerNumber, existingCustomer);

                    return true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return false;
        }

        public async Task<bool> DeleteCustomer(string email)
        {
            try
            {
                return await _customerRepository.Delete(x => x.Email == email);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return false;
        }
    }
}
