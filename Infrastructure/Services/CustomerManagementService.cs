using Infrastructure.Dtos;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using System.Diagnostics;

namespace Infrastructure.Services;

public class CustomerManagementService
{
    private readonly AddressRepository _addressRepository;
    private readonly CustomerRepository _customerRepository;
    private readonly RoleRepository _roleRepository;

    public CustomerManagementService(AddressRepository addressRepository, CustomerRepository customerRepository, RoleRepository roleRepository)
    {
        _addressRepository = addressRepository;
        _customerRepository = customerRepository;
        _roleRepository = roleRepository;
    }

    public bool CreateCustomer(CustomerRegistrationDto customer)
    {
        try
        {
            if (!_customerRepository.Exists(x => x.Email == customer.Email))
            {
                var role = _roleRepository.GetOne(x => x.RoleName == customer.RoleName);
                role ??= _roleRepository.Create(new RoleEntity
                {
                    RoleName = customer.RoleName,
                });

                var customerAddress = _addressRepository.GetOne(x =>
                x.StreetName == customer.StreetName &&
                x.City == customer.City &&
                x.PostalCode == customer.PostalCode &&
                x.Country == customer.Country);

                customerAddress ??= _addressRepository.Create(new AddressEntity
                {
                    StreetName = customer.StreetName,
                    City = customer.City,
                    PostalCode = customer.PostalCode,
                    Country = customer.Country
                });

                var newCustomer = new CustomerEntity
                {
                    CustomerNumber = Guid.NewGuid().ToString(),
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Email = customer.Email,
                    RoleId = role.RoleId,
                };

                newCustomer.CustomerAddresses.Add(new CustomerAddressEntity
                {
                    AddressId = customerAddress.AddressId
                });

                var result = _customerRepository.Create(newCustomer);
                if (result != null)
                    return true;
            }

        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        return false;
    }

    public IEnumerable<CustomerDto> GetAllCustomers()
    {
        try
        {
            var customers = new List<CustomerDto>();
            var result = _customerRepository.GetAll();
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

                    var role = _roleRepository.GetOne(x => x.RoleId == customer.RoleId);
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

    public CustomerDto GetOneCustomer(string email)
    {
        try
        {
            var newCustomer = new CustomerDto();
            var existingCustomer = _customerRepository.GetOne(x => x.Email == email);
            if (existingCustomer != null)
            {
                newCustomer.CustomerNumber = existingCustomer.CustomerNumber;
                newCustomer.FirstName = existingCustomer.FirstName;
                newCustomer.LastName = existingCustomer.LastName;
                newCustomer.Email = existingCustomer.Email;
                newCustomer.RoleName = existingCustomer.Role.RoleName;

                return newCustomer;
            }

        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        return null!;
    }

    public bool UpdateCustomer(CustomerRegistrationDto customer)
    {
        try
        {
            var existingCustomer = _customerRepository.GetOne(x => x.Email == customer.Email);

            if (existingCustomer != null)
            {
                var updateRole = _roleRepository.GetOne(x => x.RoleName == customer.RoleName);
                updateRole ??= _roleRepository.Create(new RoleEntity { RoleName = customer.RoleName });

                var updateAddress = _addressRepository.GetOne(x =>
                    x.StreetName == customer.StreetName &&
                    x.City == customer.City &&
                    x.PostalCode == customer.PostalCode &&
                    x.Country == customer.Country);
                updateAddress ??= _addressRepository.Create(new AddressEntity
                {
                    StreetName = customer.StreetName,
                    City = customer.City,
                    PostalCode = customer.PostalCode,
                    Country = customer.Country
                });

                var updatedCustomer = new CustomerEntity
                {
                    CustomerNumber = existingCustomer.CustomerNumber,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Email = customer.Email,
                    RoleId = updateRole.RoleId
                };

                existingCustomer.CustomerAddresses.Clear();

                updatedCustomer.CustomerAddresses.Add(new CustomerAddressEntity
                {
                    AddressId = updateAddress.AddressId
                });

                _customerRepository.Update(updatedCustomer);

                return true;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }

        return false;
    }

    public bool DeleteCustomer(string email)
    {
        try
        {
            var existingCustomer = _customerRepository.Delete(x => x.Email == email);
            if (existingCustomer)
            {
                return existingCustomer;
            }         
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        return false;
    }
}
