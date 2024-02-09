using Infrastructure.Dtos;
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
            while (true)
            {

                Console.WriteLine("##MENU##");
                Console.WriteLine();
                Console.WriteLine("[1] Customer Management");
                Console.WriteLine("[2] Order Management");
                Console.WriteLine("[3] Exit");
                int.TryParse(Console.ReadLine(), out int userOption);

                Console.Clear();

                switch (userOption)
                {
                    case 1:
                        Console.WriteLine("Customer menu");
                        Console.WriteLine();
                        Console.WriteLine("[1] Add Customer");
                        Console.WriteLine("[2] Search Customer");
                        Console.WriteLine("[3] Get All Customers");
                        Console.WriteLine("[4] Update Customer");
                        Console.WriteLine("[5] Delete Customer");
                        Console.WriteLine("[6] Exit");
                        int.TryParse(Console.ReadLine(), out int userCustomerOption);

                        Console.Clear();

                        switch (userCustomerOption)
                        {
                            case 1:
                                ShowAddCustomerMenu();
                                break;
                            case 2:
                                ShowCustomerSearchMenu();
                                break;
                            case 3:
                                ShowAllCustomersMenu();
                                break;
                            case 4:
                                ShowCustomerUpdateMenu();
                                break;
                            case 5:
                                ShowRemoveCustomerMenu();
                                break;
                            case 6:
                                ShowExitMenu();
                                break;

                            default:
                                Console.WriteLine("Enter a valid inpuit!");
                                break;
                        }
                        Console.ReadKey();
                        Console.Clear();
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

                        Console.Clear();
                        switch (userOrderOption)
                        {
                            case 1:
                                ShowAddOrderMenu();
                                break;
                            case 2:
                                ShowOrderSearchMenu();
                                break;
                            case 3:
                                ShowAllOrdersMenu();
                                break;
                            case 4:
                                ShowOrderUpdateMenu();
                                break;
                            case 5:
                                ShowRemoveOrderMenu();
                                break;
                            case 6:
                                ShowExitMenu();
                                break;
                            default:
                                Console.WriteLine("Enter a valid inpuit!");
                                break;
                        }
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case 3:
                        ShowExitMenu();
                        break;
                    default:
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }




    public void ShowAddOrderMenu()
    {
        try
        {
            while (true)
            {
                OrderRegistrationDto newOrder = new OrderRegistrationDto();

                ShowMenuText("Add Order");
                Console.WriteLine("-------------");
                Console.WriteLine();
                Console.Write("Customer number: ");
                var existingCustomer = _customerManagementService.GetCustomerWithCustomerNumber(Console.ReadLine()!);

                if (existingCustomer != null)
                {
                    ShowMenuText("The customer was found");
                    Console.WriteLine("-------------------------");
                    Console.WriteLine();
                    Console.WriteLine($"Customer number: {existingCustomer.CustomerNumber}");
                    Console.WriteLine($"First name: {existingCustomer.FirstName}");
                    Console.WriteLine($"Last name: {existingCustomer.LastName}");
                    Console.WriteLine($"Email: {existingCustomer.Email}");
                    Console.WriteLine($"Role name: {existingCustomer.RoleName}");
                    Console.WriteLine();

                    Console.WriteLine("[1] Add customer to new order");
                    Console.WriteLine("[2] Exit");
                    int.TryParse(Console.ReadLine(), out int userInput);

                    Console.Clear();

                    switch (userInput)
                    {
                        case 1:
                            newOrder.OrderDate = DateTime.Now;
                            newOrder.CustomerNumber = existingCustomer.CustomerNumber;
                            newOrder.FirstName = existingCustomer.FirstName;
                            newOrder.LastName = existingCustomer.LastName;
                            newOrder.Email = existingCustomer.Email;

                            Console.Write("Payment method: ");
                            newOrder.PaymentMethodName = ValidateText(Console.ReadLine()!);

                            Console.Write("Description: ");
                            newOrder.Description = Console.ReadLine()!;

                            bool savedOrder = _orderPaymentManagementService.CreateOrder(newOrder);
                            if (!savedOrder)
                            {
                                Console.WriteLine();
                                Console.WriteLine("Order already exists");
                                break;
                            }

                            Console.WriteLine();
                            ShowMenuText("Order saved");
                            Console.WriteLine("---------------");
                            break;
                        case 2:
                            ShowExitMenu();
                            break;
                        default:
                            Console.WriteLine("Not a valid input!");
                            break;
                    }
                }
                else
                {
                    ShowMenuText("No customer was found");
                    Console.WriteLine("-------------------------");
                }
                break;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }
    public void ShowAllOrdersMenu()
    {
        try
        {
            while (true)
            {
                ShowMenuText("Order list");
                Console.WriteLine("--------------\n");

                var orderList = _orderPaymentManagementService.GetAllOrders();
                if (orderList != null && orderList.Any())
                {
                    foreach (var order in orderList)
                    {

                        Console.WriteLine($"Order date: {order.OrderDate}\n" +
                                          $"Order number: {order.OrderNumber}\n" +
                                          $"Customer number: {order.CustomerNumber}\n" +
                                          $"First name: {order.Customer.FirstName}\n" +
                                          $"Last name: {order.Customer.LastName}\n" +
                                          $"Email: {order.Customer.Email}\n" +
                                          "\n-------------------\n");

                    };
                    break;
                }
                else
                {
                    Console.WriteLine("The order list is empty!");
                    Console.WriteLine("--------------------------");
                }
                break;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }
    public void ShowOrderSearchMenu()
    {
        try
        {
            ShowMenuText("Search order");
            Console.WriteLine("----------------\n");
            Console.Write("Search order with order number: ");
            int.TryParse(Console.ReadLine(), out int userInput);
            var existingOrder = _orderPaymentManagementService.GetOneOrder(userInput);

            Thread.Sleep(500);
            Console.Clear();
            if (existingOrder != null)
            {
                ShowMenuText("Order was found");
                Console.WriteLine("-------------------");
                Console.WriteLine($"Order date: {existingOrder.OrderDate}\n" +
                                  $"Customer number: {existingOrder.CustomerNumber}\n" +
                                  $"First name: {existingOrder.FirstName}\n" +
                                  $"Last name: {existingOrder.LastName}\n" +
                                  $"Email: {existingOrder.Email}\n" +
                                  $"Payment Method: {existingOrder.PaymentMethodName}\n" +
                                  $"Description: {existingOrder.Description}\n" +
                                  $"Street name: {existingOrder.StreetName}\n" +
                                  $"City: {existingOrder.City}\n" +
                                  $"Country: {existingOrder.Country}\n" +
                                  "\n-------------------\n");
            }
            else
            {
                ShowMenuText("Order was not found");
                Console.WriteLine("------------------------");
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
        }

    }
    public void ShowOrderUpdateMenu()
    {
        try
        {
            while (true)
            {
                ShowMenuText("Update order");
                Console.WriteLine("----------------\n");
                Console.Write("Order number: ");
                int.TryParse(Console.ReadLine()!, out int userInput);
                var orderToBeUpdated = _orderPaymentManagementService.GetOneOrder(userInput);
                Thread.Sleep(500);
                Console.Clear();


                if (orderToBeUpdated != null)
                {
                    ShowMenuText("Order was found");
                    Console.WriteLine("-------------------");
                    Console.WriteLine();
                    Console.WriteLine($"Order date: {orderToBeUpdated.OrderDate}\nCustomer number: {orderToBeUpdated.CustomerNumber}\nFirst name: {orderToBeUpdated.FirstName}\nLast name: {orderToBeUpdated.LastName}\nEmail: {orderToBeUpdated.Email}\nPayment method: {orderToBeUpdated.PaymentMethodName}\nDescription: {orderToBeUpdated.Description}");
                    Console.Write("\nDo you want to update? (y/n): ");
                    string OrderUpdateInput = ValidateText(Console.ReadLine()!);
                    Thread.Sleep(500);
                    Console.Clear();

                    if (OrderUpdateInput == "y")
                    {
                        Console.Write("Paymentmethod name:: ");
                        var validatedPaymentmethod = Console.ReadLine()!;
                        orderToBeUpdated.PaymentMethodName = validatedPaymentmethod;

                        Console.Write("Description: ");
                        var validatedDescription = Console.ReadLine()!;
                        orderToBeUpdated.Description = validatedDescription;

                        _orderPaymentManagementService.UpdateOrder(orderToBeUpdated, userInput);

                        ShowMenuText("Order was updated");

                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    ShowMenuText("Order was not found");
                    Console.WriteLine("------------------------");
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }
    public void ShowRemoveOrderMenu()
    {
        try
        {
            while (true)
            {
                ShowMenuText("Remove order");
                Console.WriteLine("-----------------\n");
                Console.Write("Remove order with order number: ");
                int.TryParse(Console.ReadLine()!, out int orderRemoveInput);
                var orderToRemoveFound = _orderPaymentManagementService.GetOneOrder(orderRemoveInput);

                if (orderToRemoveFound != null)
                {
                    Console.WriteLine($"Order number: {orderToRemoveFound.OrderNumber}\nOrder date: {orderToRemoveFound.OrderDate}\nCustomer number: {orderToRemoveFound.CustomerNumber}\nFirst name: {orderToRemoveFound.FirstName}\nLast name: {orderToRemoveFound.LastName}\nEmail: {orderToRemoveFound.Email}");
                    Console.WriteLine();
                    Console.Write("Do you want to remove? (y/n): ");
                    string orderRemoveInputAnswer = ValidateText(Console.ReadLine()!);
                    Thread.Sleep(500);
                    Console.Clear();
                    if (orderRemoveInputAnswer == "y")
                    {
                        var removeCustomer = _orderPaymentManagementService.DeleteOrder(orderRemoveInput);
                        ShowMenuText("Order was removed");
                        Thread.Sleep(1000);
                        break;
                    }
                }
                if (orderToRemoveFound == null)
                {
                    Console.WriteLine();
                    Console.WriteLine("Order was not found!");
                    break;
                }
                break;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }


    public void ShowAddCustomerMenu()
    {
        try
        {
            while (true)
            {
                CustomerRegistrationDto newCustomer = new CustomerRegistrationDto();

                ShowMenuText("Add customer");
                Console.WriteLine("-------------");
                Console.WriteLine();

                Console.Write("First name: ");
                var validatedFirstname = Console.ReadLine()!;
                validatedFirstname = ValidateText(validatedFirstname);
                if (validatedFirstname == null) { break; }
                newCustomer.FirstName = validatedFirstname;

                Console.Write("Last name: ");
                var validatedLastName = Console.ReadLine()!;
                validatedLastName = ValidateText(validatedLastName);
                if (validatedLastName == null) { break; }
                newCustomer.LastName = validatedLastName;


                Console.Write("Email: ");
                var validatedEmail = Console.ReadLine()!;
                validatedEmail = ValidateEmail(validatedEmail);
                if (validatedEmail == null) { break; }
                newCustomer.Email = validatedEmail;

                Console.Write("Role name: ");
                var validatedRoleName = Console.ReadLine()!;
                validatedRoleName = ValidateText(validatedRoleName);
                if (validatedRoleName == null) { break; }
                newCustomer.RoleName = validatedRoleName;

                Console.Write("Street name: ");
                var validatedStreetName = Console.ReadLine()!;
                if (validatedStreetName == null) { break; }
                newCustomer.StreetName = validatedStreetName;


                Console.Write("City: ");
                var validatedCity = Console.ReadLine()!;
                validatedCity = ValidateText(validatedCity);
                if (validatedCity == null) { break; }
                newCustomer.City = validatedCity;

                Console.Write("Postal code: ");
                var validatedPostalCode = Console.ReadLine()!;
                validatedPostalCode = ValidateNum(validatedPostalCode);
                if (validatedPostalCode == null) { break; }
                newCustomer.PostalCode = validatedPostalCode;

                Console.Write("Country: ");
                var validatedCountry = Console.ReadLine()!;
                validatedCountry = ValidateText(validatedCountry);
                if (validatedCountry == null) { break; }
                newCustomer.Country = validatedCountry;

                var savedCustomer = _customerManagementService.CreateCustomer(newCustomer);
                if (savedCustomer != true)
                {
                    Console.WriteLine();
                    Console.WriteLine("Customer already in list");
                    break;
                }
                Console.WriteLine();
                ShowMenuText("Customer saved");
                Console.WriteLine("---------------");

                break;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }
    public void ShowAllCustomersMenu()
    {
        try
        {
            while (true)
            {
                ShowMenuText("Customer list");
                Console.WriteLine("--------------\n");
                var customerList = _customerManagementService.GetAllCustomers();
                if (customerList != null && customerList.Any())
                {
                    foreach (var customer in customerList)
                    {
                        Console.WriteLine($"Customer number: {customer.CustomerNumber}\n" +
                                          $"First name: {customer.FirstName}\n" +
                                          $"Last name: {customer.LastName}\n" +
                                          $"Email: {customer.Email}\n" +
                                          $"Role name: {customer.RoleName}\n" +
                                          "\n-------------------\n");
                    }
                    break;
                }
                else
                {
                    Console.WriteLine("The contact list is empty!");
                    Console.WriteLine("--------------------------");
                }
                break;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }
    public void ShowCustomerSearchMenu()
    {
        try
        {
            ShowMenuText("Search customer");
            Console.WriteLine("----------------\n");
            Console.WriteLine("[1] Search with email");
            Console.WriteLine("[2] Search with customer number");
            int.TryParse(Console.ReadLine(), out int result);

            switch (result)
            {
                case 1:
                    Console.Write("Search customer with email: ");
                    var customerFound = _customerManagementService.GetOneCustomer(ValidateEmail(Console.ReadLine()!));
                    Thread.Sleep(500);
                    Console.Clear();
                    if (customerFound != null)
                    {
                        ShowMenuText("Contact was found");
                        Console.WriteLine("-------------------");
                        Console.WriteLine($"First name: {customerFound.FirstName}\n" +
                                          $"Last name: {customerFound.LastName}\n" +
                                          $"Email: {customerFound.Email}\n" +
                                          $"Street name: {customerFound.StreetName}\n" +
                                          $"City: {customerFound.City}\n" +
                                          $"Postalcode: {customerFound.PostalCode}\n" +
                                          $"Country: {customerFound.Country}\n" +
                                          $"Role name: {customerFound.RoleName}\n" +
                                          "\n-------------------\n");
                    }
                    else
                    {
                        ShowMenuText("customer was not found");
                        Console.WriteLine("------------------------");
                    }
                    break;
                case 2:
                    Console.Write("Search customer with customer number: ");
                    var customer = _customerManagementService.GetCustomerWithCustomerNumber(Console.ReadLine()!);
                    Thread.Sleep(500);
                    Console.Clear();
                    if (customer != null)
                    {
                        ShowMenuText("Contact was found");
                        Console.WriteLine("-------------------");
                        Console.WriteLine($"First name: {customer.FirstName}\n" +
                                          $"Last name: {customer.LastName}\n" +
                                          $"Email: {customer.Email}\n" +
                                          $"Role name: {customer.RoleName}\n" +
                                          "\n-------------------\n");
                    }
                    else
                    {
                        ShowMenuText("customer was not found");
                        Console.WriteLine("------------------------");
                    }
                    break;
                default:
                    Console.WriteLine("Enter a valid input");
                    break;
            }

        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
        }

    }
    public void ShowCustomerUpdateMenu()
    {
        try
        {
            while (true)
            {
                ShowMenuText("Update customer");
                Console.WriteLine("----------------\n");
                Console.Write("Update customer with email: ");
                string customerUpdateEmailInput = ValidateEmail(Console.ReadLine()!);
                var customerToBeUpdated = _customerManagementService.GetOneCustomer(customerUpdateEmailInput);
                Thread.Sleep(500);
                Console.Clear();


                if (customerToBeUpdated != null)
                {
                    ShowMenuText("Contact was found");
                    Console.WriteLine("-------------------");
                    Console.WriteLine();
                    Console.WriteLine($"First name: {customerToBeUpdated.FirstName}\n" +
                                          $"Last name: {customerToBeUpdated.LastName}\n" +
                                          $"Email: {customerToBeUpdated.Email}\n" +
                                          $"Street name: {customerToBeUpdated.StreetName}\n" +
                                          $"City: {customerToBeUpdated.City}\n" +
                                          $"Postalcode: {customerToBeUpdated.PostalCode}\n" +
                                          $"Country: {customerToBeUpdated.Country}\n" +
                                          $"Role name: {customerToBeUpdated.RoleName}\n" +
                                          "\n-------------------\n");

                    Console.Write("\nDo you want to update? (y/n): ");
                    string CustomerUpdateAnswerInput = ValidateText(Console.ReadLine()!);
                    Thread.Sleep(500);
                    Console.Clear();

                    if (CustomerUpdateAnswerInput == "y")
                    {
                        Console.Write("First name: ");
                        var validatedFirstname = Console.ReadLine()!;
                        validatedFirstname = ValidateText(validatedFirstname);
                        customerToBeUpdated.FirstName = validatedFirstname;

                        Console.Write("Last name: ");
                        var validatedLastName = Console.ReadLine()!;
                        validatedLastName = ValidateText(validatedLastName);
                        customerToBeUpdated.LastName = validatedLastName;

                        Console.Write("Email: ");
                        var validatedEmail = Console.ReadLine()!;
                        validatedEmail = ValidateEmail(validatedEmail);
                        customerToBeUpdated.Email = validatedEmail;

                        Console.Write("Role name: ");
                        var validatedRoleName = Console.ReadLine()!;
                        validatedRoleName = ValidateText(validatedRoleName);
                        customerToBeUpdated.RoleName = validatedRoleName;

                        Console.Write("Street name: ");
                        var validatedStreetName = Console.ReadLine()!;
                        customerToBeUpdated.StreetName = validatedStreetName;

                        Console.Write("City: ");
                        var validatedCity = Console.ReadLine()!;
                        validatedCity = ValidateText(validatedCity);
                        customerToBeUpdated.City = validatedCity;

                        Console.Write("Postal code: ");
                        var validatedPostalCode = Console.ReadLine()!;
                        validatedPostalCode = ValidateNum(validatedPostalCode);
                        customerToBeUpdated.PostalCode = validatedPostalCode;

                        Console.Write("Country: ");
                        var validatedCountry = Console.ReadLine()!;
                        validatedCountry = ValidateText(validatedCountry);
                        customerToBeUpdated.Country = validatedCountry;

                        _customerManagementService.UpdateCustomer(customerToBeUpdated, customerUpdateEmailInput);

                        ShowMenuText("Contact was updated");
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    ShowMenuText("Customer was not found");
                    Console.WriteLine("------------------------");
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }
    public void ShowRemoveCustomerMenu()
    {
        try
        {

            ShowMenuText("Delete customer");
            Console.WriteLine("-----------------\n");
            Console.WriteLine("[1] Remove customer with email");
            Console.WriteLine("[2] Remove customer with customer number");
            int.TryParse(Console.ReadLine(), out var result);

            switch (result)
            {
                case 1:
                    Console.Write("Remove customer with email: ");
                    string customerRemoveInput = ValidateEmail(Console.ReadLine()!);
                    var customerToRemoveFound = _customerManagementService.GetOneCustomer(customerRemoveInput);

                    if (customerToRemoveFound != null)
                    {
                        Console.WriteLine($"\nFirst name: {customerToRemoveFound.FirstName}\nLast name: {customerToRemoveFound.LastName}\nEmail: {customerToRemoveFound.Email}\nRole name: {customerToRemoveFound.RoleName}\nStreet name: {customerToRemoveFound.StreetName}\nCity: {customerToRemoveFound.City}\nPostal code: {customerToRemoveFound.PostalCode}\nCountry: {customerToRemoveFound.Country}");
                        Console.WriteLine();
                        Console.Write("Do you want to remove? (y/n): ");
                        string customerRemoveInputAnswer = ValidateText(Console.ReadLine()!);
                        Thread.Sleep(500);
                        Console.Clear();
                        if (customerRemoveInputAnswer == "y")
                        {
                            var removeCustomer = _customerManagementService.DeleteCustomer(customerRemoveInput);
                            ShowMenuText("Customer was removed");
                            Thread.Sleep(1000);
                        }
                    }
                    if (customerToRemoveFound == null)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Customer was not found!");
                    }
                    break;
                case 2:
                    Console.Write("Remove customer with customer number: ");
                    string userRemoveInput = Console.ReadLine()!;
                    var customerToRemove = _customerManagementService.GetCustomerWithCustomerNumber(userRemoveInput);

                    if (customerToRemove != null)
                    {
                        Console.WriteLine($"\nFirst name: {customerToRemove.FirstName}\nLast name: {customerToRemove.LastName}\nEmail: {customerToRemove.Email}\nRole name: {customerToRemove.RoleName}");
                        Console.WriteLine();
                        Console.Write("Do you want to remove? (y/n): ");
                        string customerRemoveInputAnswer = ValidateText(Console.ReadLine()!);
                        Thread.Sleep(500);
                        Console.Clear();
                        if (customerRemoveInputAnswer == "y")
                        {
                            var removeCustomer = _customerManagementService.DeleteCustomer(customerToRemove.Email);
                            ShowMenuText("Customer was removed");
                            Thread.Sleep(1000);
                        }
                    }
                    if (customerToRemove == null)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Customer was not found!");
                    }
                    break;
                default:
                    Console.WriteLine("Enter a valid input");
                    break;
            }


        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }


    public static void ShowExitMenu()
    {
        try
        {
            Console.Write("Do you want to exit? (y/n): ");

            string userExitInput = ValidateText(Console.ReadLine()!);
            if (userExitInput == "y")
            {
                Environment.Exit(0);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
        }

    }
    public static void ShowMenuText(string text)
    {
        Console.WriteLine($"\n[{text}]");
    }

    public static string ValidateText(string text)
    {
        if (!string.IsNullOrWhiteSpace(text) && !text.Any(char.IsDigit))
        {
            return text.ToLower();
        }

        Console.WriteLine("Invalid input!");
        return null!;
    }

    public static string ValidateNum(string text)
    {
        if (!string.IsNullOrWhiteSpace(text))
        {
            return text.ToLower();
        }

        Console.WriteLine("Invalid input!");
        return null!;
    }

    public static string ValidateEmail(string text)
    {
        if (!string.IsNullOrWhiteSpace(text) && text.Contains("@"))
        {
            return text.ToLower();
        }

        Console.WriteLine("Invalid input!");
        return null!;
    }
}
