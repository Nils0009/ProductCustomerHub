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
			Console.WriteLine();
			Console.WriteLine("[1] Customer Management");
			Console.WriteLine("[2] Order Management");
            int.TryParse(Console.ReadLine(), out int userOption);

			switch (userOption)
			{
				case 1:
					Console.WriteLine("Customer menu");
					Console.WriteLine();
					Console.WriteLine("[1] Add Customer");
                    Console.WriteLine("[2] Get Customer");
                    Console.WriteLine("[3] Get All Customers");
                    Console.WriteLine("[4] Update Customer");
                    Console.WriteLine("[5] Delete Customer");
                    Console.WriteLine("[6] Exit");
                    int.TryParse (Console.ReadLine(), out int userCustomerOption);

                    switch (userCustomerOption)
                    {
                        case 1:
                            break;
                        case 2:
                            break;
                        case 3:
                            break;
                        case 4:
                            break;
                        case 5:
                            break;
                        case 6:
                            break;
                        default:
                            break;
                    }
                    break;

				case 2:
                    Console.WriteLine("Order menu");
                    Console.WriteLine();
                    Console.WriteLine("[1] Add Order");
                    Console.WriteLine("[2] Get Order");
                    Console.WriteLine("[3] Get All Orders");
                    Console.WriteLine("[4] Update Order");
                    Console.WriteLine("[5] Delete Order");
                    Console.WriteLine("[6] Exit");

                    int.TryParse(Console.ReadLine(), out int userOrderOption);
                    switch (userOrderOption)
                    {
                        case 1:
                            break;
                        case 2:
                            break;
                        case 3:
                            break;
                        case 4:
                            break;
                        case 5:
                            break;
                        case 6:
                            break;
                        default:
                            break;
                    }
                    break;
				default:
					break;
			}
		}
		catch (Exception ex)
		{
			Debug.WriteLine(ex.Message);
		}
    };
}
