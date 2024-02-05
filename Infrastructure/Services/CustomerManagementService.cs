using Infrastructure.Dtos;
using Infrastructure.Repositories;
using System.Diagnostics;

namespace Infrastructure.Services;

public class CustomerManagementService
{
    private readonly AddressRepository _addressRepository;
    private readonly CustomerAddressRepository _customerAddressRepository;
    private readonly CustomerRepository _customerRepository;
    private readonly RoleRepository _roleRepository;

    public CustomerManagementService(AddressRepository addressRepository, CustomerAddressRepository customerAddressRepository, CustomerRepository customerRepository, RoleRepository roleRepository)
    {
        _addressRepository = addressRepository;
        _customerAddressRepository = customerAddressRepository;
        _customerRepository = customerRepository;
        _roleRepository = roleRepository;
    }

    public bool CreateCustomer(CustomerDto customer)
    {
        try
        {

        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        return false;
    }
}
