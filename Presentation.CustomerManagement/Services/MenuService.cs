using Infrastructure.Services;
using System.Diagnostics;

namespace Presentation.CustomerManagement.Services;

public class MenuService(CustomerManagementService customerManagementService, OrderPaymentManagementService orderPaymentManagementService)
{
    private readonly CustomerManagementService _customerManagementService = customerManagementService;
    private readonly OrderPaymentManagementService _orderPaymentManagementService = orderPaymentManagementService;

    public void ShowMainMenu()
    {
		try
		{
			Console.WriteLine("##MENU##");
		}
		catch (Exception ex)
		{
			Debug.WriteLine(ex.Message);
		}
    };
}
