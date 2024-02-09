using Infrastructure.Dtos;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using System.Diagnostics;

namespace Infrastructure.Services;

public class OrderPaymentManagementService(OrderRepository orderRepository, PaymentMethodRepository paymentMethodRepository, CustomerRepository customerRepository)
{
    private readonly OrderRepository _orderRepository = orderRepository;
    private readonly PaymentMethodRepository _paymentMethodRepository = paymentMethodRepository;
    private readonly CustomerRepository _customerRepository = customerRepository;


    public bool CreateOrder(OrderRegistrationDto order)
    {
        try
        {
            var existingCustomer = _customerRepository.GetOne(x => x.CustomerNumber == order.CustomerNumber);

            if (existingCustomer != null)
            {
                existingCustomer.FirstName = order.FirstName;
                existingCustomer.LastName = order.LastName;
                existingCustomer.Email = order.Email;

                var newOrder = new OrderEntity
                {
                    OrderDate = DateTime.Now,
                    CustomerNumber = order.CustomerNumber,
                    Customer = existingCustomer,
                };

                var paymentMethod = _paymentMethodRepository.GetOne(x => x.PaymentMethodName == order.PaymentMethodName);
                if (paymentMethod == null)
                {
                    paymentMethod = _paymentMethodRepository.Create(new PaymentMethodEntity
                    {
                        PaymentMethodName = order.PaymentMethodName,
                        Description = order.Description,
                        Orders = new List<OrderEntity> { newOrder }
                    });
                }
                else
                {
                    paymentMethod.Orders.Add(newOrder);
                }

                _orderRepository.Create(newOrder);

                return true;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
        return false;
    }
    public IEnumerable<OrderEntity> GetAllOrders()
    {
        try
        {
            var orders = _orderRepository.GetAll();
            var customers = _customerRepository.GetAll();

            if (orders != null && customers != null)
            {
                foreach (var order in orders)
                {
                    var matchingCustomer = customers.FirstOrDefault(customer => customer.CustomerNumber == order.CustomerNumber);

                    if (matchingCustomer != null)
                    {
                        order.Customer = matchingCustomer;
                    }
                }

                return orders;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }

        return Enumerable.Empty<OrderEntity>();
    }
    public OrderDto GetOneOrder(int orderNumber)
    {
        try
        {
            var existingOrder = _orderRepository.GetOne(x => x.OrderNumber == orderNumber);

            if (existingOrder != null)
            {
                var order = new OrderDto
                {
                    OrderDate = existingOrder.OrderDate,
                    CustomerNumber = existingOrder.CustomerNumber,
                    FirstName = existingOrder.Customer.FirstName,
                    LastName = existingOrder.Customer.LastName,
                    Email = existingOrder.Customer.Email,
                };

                var firstAddress = existingOrder.Customer.CustomerAddresses.FirstOrDefault();

                if (firstAddress != null)
                {
                    order.StreetName = firstAddress.Address.StreetName;
                    order.City = firstAddress.Address.City;
                    order.PostalCode = firstAddress.Address.PostalCode;
                    order.Country = firstAddress.Address.Country;
                }

                var existingPaymentMethod = _paymentMethodRepository.GetOne(x => x.Orders.Any(o => o.OrderNumber == orderNumber));
                if (existingPaymentMethod != null)
                {
                    order.PaymentMethodName = existingPaymentMethod.PaymentMethodName;
                    order.Description = existingPaymentMethod.Description;
                }
                return order;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }

        return null!;
    }
    public bool UpdateOrder(OrderDto order, int orderNumber)
    {
        try
        {
            var existingOrder = _orderRepository.GetOne(x => x.OrderNumber == orderNumber);
            if (existingOrder != null)
            {

                existingOrder.OrderDate = order.OrderDate;
                existingOrder.CustomerNumber = order.CustomerNumber;

                var existingPaymentMethod = _paymentMethodRepository.GetOne(x => x.Orders.Any(o => o.OrderNumber == orderNumber));

                if (existingPaymentMethod != null)
                {
                    existingPaymentMethod.PaymentMethodName = order.PaymentMethodName;
                    existingPaymentMethod.Description = order.Description;

                    _paymentMethodRepository.Update(x => x.PaymentMethodId == existingPaymentMethod.PaymentMethodId, existingPaymentMethod);
                }
                else
                {
                    existingPaymentMethod = _paymentMethodRepository.Create(new PaymentMethodEntity
                    {
                        PaymentMethodName = order.PaymentMethodName,
                        Description = order.Description,
                        Orders = new List<OrderEntity> { existingOrder }
                    });
                }

                _orderRepository.Update(x => x.OrderNumber == orderNumber, existingOrder);

                return true;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }

        return false;
    }
    public bool DeleteOrder(int orderNumber)
    {
        try
        {
            var existingOrder = _orderRepository.GetOne(x => x.OrderNumber == orderNumber);
            if (existingOrder != null)
            {
                var existingPaymentMethod = _paymentMethodRepository.GetOne(x => x.Orders == x.Orders);
                if (existingPaymentMethod != null)
                {
                    _paymentMethodRepository.Delete(x => x.PaymentMethodId == existingPaymentMethod.PaymentMethodId);
                    _orderRepository.Delete(x => x.OrderNumber == orderNumber);
                }
            }
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }

        return false;
    }
}

