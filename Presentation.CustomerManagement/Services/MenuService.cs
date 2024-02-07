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
                validatedStreetName = ValidateNum(validatedStreetName);
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
                                  $"Role name: {customerFound.RoleName}\n" +
                                  "\n-------------------\n");
            }
            else
            {
                ShowMenuText("customer was not found");
                Console.WriteLine("------------------------");
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
                    Console.WriteLine($"First name: {customerToBeUpdated.FirstName}\nLast name: {customerToBeUpdated.LastName}\nEmail: {customerToBeUpdated.Email}");
                    Console.Write("\nDo you want to update? (y/n): ");
                    string CustomerUpdateAnswerInput = ValidateText(Console.ReadLine()!);
                    Thread.Sleep(500);
                    Console.Clear();

                    if (CustomerUpdateAnswerInput == "y")
                    {
                        Console.Write("First name: ");
                        var validatedFirstname = Console.ReadLine()!;
                        validatedFirstname = ValidateText(validatedFirstname);
                        if (validatedFirstname == null) { break; }
                        customerToBeUpdated.FirstName = validatedFirstname;

                        Console.Write("Last name: ");
                        var validatedLastName = Console.ReadLine()!;
                        validatedLastName = ValidateText(validatedLastName);
                        if (validatedLastName == null) { break; }
                        customerToBeUpdated.LastName = validatedLastName;

                        Console.Write("Email: ");
                        var validatedEmail = Console.ReadLine()!;
                        validatedEmail = ValidateEmail(validatedEmail);
                        if (validatedEmail == null) { break; }
                        customerToBeUpdated.Email = validatedEmail;

                        Console.Write("Role name: ");
                        var validatedRoleName = Console.ReadLine()!;
                        validatedRoleName = ValidateText(validatedRoleName);
                        if (validatedRoleName == null) { break; }
                        customerToBeUpdated.RoleName = validatedRoleName;

                        Console.Write("Street name: ");
                        var validatedStreetName = Console.ReadLine()!;
                        validatedStreetName = ValidateText(validatedStreetName);
                        if (validatedStreetName == null) { break; }
                        customerToBeUpdated.StreetName = validatedStreetName;

                        Console.Write("City: ");
                        var validatedCity = Console.ReadLine()!;
                        validatedCity = ValidateText(validatedCity);
                        if (validatedCity == null) { break; }
                        customerToBeUpdated.City = validatedCity;

                        Console.Write("Postal code: ");
                        var validatedPostalCode = Console.ReadLine()!;
                        validatedPostalCode = ValidateNum(validatedPostalCode);
                        if (validatedPostalCode == null) { break; }
                        customerToBeUpdated.PostalCode = validatedPostalCode;

                        Console.Write("Country: ");
                        var validatedCountry = Console.ReadLine()!;
                        validatedCountry = ValidateText(validatedCountry);
                        if (validatedCountry == null) { break; }
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
            while (true)
            {
                ShowMenuText("Delete customer");
                Console.WriteLine("-----------------\n");
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
                        break;
                    }
                }
                if (customerToRemoveFound == null)
                {
                    Console.WriteLine();
                    Console.WriteLine("Customer was not found!");
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
        if (!string.IsNullOrEmpty(text) && !text!.Any(char.IsDigit))
        {
            return text!.ToLower();
        }

        else
        {
            Console.WriteLine("Invalid input!");
            return null!;
        }

    }
    public static string ValidateNum(string text)
    {
        if (!string.IsNullOrEmpty(text))
        {
            return text!.ToLower();
        }

        else
        {
            Console.WriteLine("Invalid input!");
            return null!;
        }

    }
    public static string ValidateEmail(string text)
    {
        if (!string.IsNullOrEmpty(text) && text!.Contains("@"))
        {
            return text!.ToLower();
        }

        else
        {
            Console.WriteLine("Invalid input!");
            return null!;
        }
    }
}
