using Infrastructure.Repositories;

namespace Infrastructure.Services;

public class OrderPaymentManagementService
{
    private readonly OrderRepository _orderRepository;
    private readonly PaymentMethodRepository _paymentMethodRepository;

    public OrderPaymentManagementService(OrderRepository orderRepository, PaymentMethodRepository paymentMethodRepository)
    {
        _orderRepository = orderRepository;
        _paymentMethodRepository = paymentMethodRepository;
    }
}
