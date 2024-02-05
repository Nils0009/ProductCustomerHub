using Infrastructure.Repositories;

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
}
