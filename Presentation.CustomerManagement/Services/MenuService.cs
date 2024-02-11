using Infrastructure.Dtos;
using Infrastructure.Entities;
using Infrastructure.Services;
using System.Diagnostics;

namespace Presentation.CustomerManagement.Services;

public class MenuService(CustomerManagementService customerManagementService, OrderPaymentManagementService orderPaymentManagementService, ProductCatalogService productCatalogService)
{
    private readonly CustomerManagementService _customerManagementService = customerManagementService;
    private readonly OrderPaymentManagementService _orderPaymentManagementService = orderPaymentManagementService;
    private readonly ProductCatalogService _productCatalogService = productCatalogService;

    public async Task ShowMainMenu()
    {
        try
        {
            while (true)
            {
                Console.WriteLine("##MENU##");
                Console.WriteLine();
                Console.WriteLine("[1] Customer Management");
                Console.WriteLine("[2] Order Management");
                Console.WriteLine("[3] Product Management");
                Console.WriteLine("[4] Exit");
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
                                await ShowAddCustomerMenu();
                                break;
                            case 2:
                                await ShowCustomerSearchMenu();
                                break;
                            case 3:
                                await ShowAllCustomersMenu();
                                break;
                            case 4:
                                await ShowCustomerUpdateMenu();
                                break;
                            case 5:
                                await ShowRemoveCustomerMenu();
                                break;
                            case 6:
                                ShowExitMenu();
                                break;

                            default:
                                Console.WriteLine("Enter a valid input!");
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
                                await ShowAddOrderMenu();
                                break;
                            case 2:
                                await ShowOrderSearchMenu();
                                break;
                            case 3:
                                await ShowAllOrdersMenu();
                                break;
                            case 4:
                                await ShowOrderUpdateMenu();
                                break;
                            case 5:
                                await ShowRemoveOrderMenu();
                                break;
                            case 6:
                                ShowExitMenu();
                                break;
                            default:
                                Console.WriteLine("Enter a valid input!");
                                break;
                        }
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case 3:
                        Console.WriteLine("Product menu");
                        Console.WriteLine();
                        Console.WriteLine("[1] Add Product");
                        Console.WriteLine("[2] Get Product");
                        Console.WriteLine("[3] Get All Products");
                        Console.WriteLine("[4] Update Product");
                        Console.WriteLine("[5] Delete Product");
                        Console.WriteLine("[6] Exit");
                        int.TryParse(Console.ReadLine(), out int userProductOption);

                        Console.Clear();
                        switch (userProductOption)
                        {
                            case 1:
                                await ShowAddProductMenu();
                                break;
                            case 2:
                                await ShowProductSearchMenu();
                                break;
                            case 3:
                                await ShowAllProductsMenu();
                                break;
                            case 4:
                                await ShowProductUpdateMenu();
                                break;
                            case 5:
                                await ShowRemoveProductMenu();
                                break;
                            case 6:
                                ShowExitMenu();
                                break;
                            default:
                                Console.WriteLine("Enter a valid input!");
                                break;
                        }
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case 4:
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


    public async Task ShowAddProductMenu()
    {
        try
        {
            while (true)
            {
                ProductEntity newProduct = new ProductEntity();
                CategoryEntity newCategory = new CategoryEntity();
                ManufacturerEntity newManufacturer = new ManufacturerEntity();
                PriceEntity newPrice = new PriceEntity();

                ShowMenuText("Add Product");
                Console.WriteLine("-------------");
                Console.WriteLine();

                Console.Write("Title: ");
                var validatedTitle = Console.ReadLine()!;
                newProduct.Title = validatedTitle;

                Console.Write("Description: ");
                var validatedDescription = Console.ReadLine()!;
                newProduct.Description = validatedDescription;

                Console.Write("Category: ");
                var validatedCategory = Console.ReadLine()!;
                validatedCategory = ValidateText(validatedCategory);
                if (validatedCategory == null) { break; }
                newCategory.CategoryName = validatedCategory;
                newProduct.Category = newCategory; // Tilldela rätt instans till newProduct.Category

                Console.Write("Manufacturer: ");
                var validatedManufacturer = Console.ReadLine()!;
                validatedManufacturer = ValidateText(validatedManufacturer);
                if (validatedManufacturer == null) { break; }
                newManufacturer.Manufacturer = validatedManufacturer;
                newProduct.Manufacturer = newManufacturer; // Tilldela rätt instans till newProduct.Manufacturer

                Console.Write("Price: ");
                int.TryParse(Console.ReadLine()!, out int validatedPrice);
                newPrice.UnitPrice = validatedPrice;
                newProduct.Price = newPrice; // Tilldela rätt instans till newProduct.Price

                var savedProduct = await _productCatalogService.CreateProduct(newProduct);
                if (savedProduct == false)
                {
                    Console.WriteLine();
                    Console.WriteLine("Product already in the list");
                    break;
                }

                Console.WriteLine();
                ShowMenuText("Product saved");
                Console.WriteLine("---------------");

                break;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }
    public async Task ShowAllProductsMenu()
    {
        try
        {
            while (true)
            {
                ShowMenuText("Products list");
                Console.WriteLine("--------------\n");

                var productList = await _productCatalogService.GetAllProducts();
                if (productList != null && productList.Any())
                {
                    foreach (var product in productList)
                    {
                        Console.WriteLine($"Article Number: {product.ArticleNumber}\n" +
                                          $"Title: {product.Title}\n" +
                                          $"Description: {product.Description}\n" +
                                          "\n-------------------\n");
                    };
                    break;
                }
                else
                {
                    Console.WriteLine("The product list is empty!");
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
    public async Task ShowProductSearchMenu()
    {
        try
        {
            ShowMenuText("Search product");
            Console.WriteLine("----------------\n");
            Console.Write("Search with article number: ");
            var userInput = Console.ReadLine();
            if (userInput != null)
            {
                var existingProduct = await _productCatalogService.GetOneProduct(userInput);
                if (existingProduct != null)
                {
                    ShowMenuText("Product was found");
                    Console.WriteLine("-------------------");
                    Console.WriteLine($"Article number: {existingProduct.ArticleNumber}\n" +
                                      $"Title: {existingProduct.Title}\n" +
                                      $"Description: {existingProduct.Description}\n" +
                                      $"Manufacturer: {existingProduct.Manufacturer.Manufacturer}\n" +
                                      $"Category: {existingProduct.Category.CategoryName}\n" +
                                      $"Price: {existingProduct.Price.UnitPrice}\n" +
                                      "\n-------------------\n");
                }
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
        }
    }
    public async Task ShowProductUpdateMenu()
    {
        try
        {
            while (true)
            {
                ShowMenuText("Update product");
                Console.WriteLine("----------------\n");
                Console.Write("Update product with article number: ");
                string productUpdateInput = Console.ReadLine()!;
                var productToBeUpdated = await _productCatalogService.GetOneProduct(productUpdateInput);
                Thread.Sleep(500);
                Console.Clear();

                if (productToBeUpdated != null)
                {
                    ShowMenuText("Product was found");
                    Console.WriteLine("-------------------");
                    Console.WriteLine($"Article number: {productToBeUpdated.ArticleNumber}\n" +
                                      $"Title: {productToBeUpdated.Title}\n" +
                                      $"Description: {productToBeUpdated.Description}\n" +
                                      $"Manufacturer: {productToBeUpdated.Manufacturer.Manufacturer}\n" +
                                      $"Category: {productToBeUpdated.Category.CategoryName}\n" +
                                      $"Price: {productToBeUpdated.Price.UnitPrice}\n" +
                                      "\n-------------------\n");

                    Console.Write("\nDo you want to update? (y/n): ");
                    string ProductUpdateAnswerInput = ValidateText(Console.ReadLine()!);
                    Thread.Sleep(500);
                    Console.Clear();

                    if (ProductUpdateAnswerInput == "y")
                    {
                        Console.Write("Title: ");
                        var validatedTitle = Console.ReadLine()!;
                        validatedTitle = ValidateAll(validatedTitle);
                        productToBeUpdated.Title = validatedTitle;

                        Console.Write("Description: ");
                        var validatedDescription = Console.ReadLine()!;
                        validatedDescription = ValidateAll(validatedDescription);
                        productToBeUpdated.Description = validatedDescription;

                        Console.Write("Manufacturer: ");
                        var validatedManufacturer = Console.ReadLine()!;
                        validatedManufacturer = ValidateAll(validatedManufacturer);
                        productToBeUpdated.Manufacturer.Manufacturer = validatedManufacturer;

                        Console.Write("Category: ");
                        var validatedCategory = Console.ReadLine()!;
                        validatedCategory = ValidateAll(validatedCategory);
                        productToBeUpdated.Category.CategoryName = validatedCategory;

                        Console.Write("Price: ");
                        int.TryParse(Console.ReadLine()!, out int validatedPrice);
                        productToBeUpdated.Price.UnitPrice = validatedPrice;

                        await _productCatalogService.UpdateProduct(productToBeUpdated, productUpdateInput);

                        ShowMenuText("Product was updated");
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    ShowMenuText("Product was not found");
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
    public async Task ShowRemoveProductMenu()
    {
        try
        {
            ShowMenuText("Delete product");
            Console.WriteLine("-----------------\n");
            Console.Write("Remove product with article number: ");
            string userRemoveInput = Console.ReadLine()!;
            var productToRemove = await _productCatalogService.GetOneProduct(userRemoveInput);

            if (productToRemove != null)
            {
                ShowMenuText("Product was found");
                Console.WriteLine("-------------------");
                Console.WriteLine($"Article number: {productToRemove.ArticleNumber}\n" +
                                  $"Title: {productToRemove.Title}\n" +
                                  $"Description: {productToRemove.Description}\n" +
                                  $"Manufacturer: {productToRemove.Manufacturer.Manufacturer}\n" +
                                  $"Category: {productToRemove.Category.CategoryName}\n" +
                                  $"Price: {productToRemove.Price.UnitPrice}\n" +
                                  "\n-------------------\n");

                Console.Write("Do you want to remove? (y/n): ");
                string productRemoveInputAnswer = ValidateText(Console.ReadLine()!);
                Thread.Sleep(500);
                Console.Clear();
                if (productRemoveInputAnswer == "y")
                {
                    var removeProduct = await _productCatalogService.DeleteProduct(userRemoveInput);
                    ShowMenuText("Product was removed");
                    Thread.Sleep(1000);
                }
            }
            if (productToRemove == null)
            {
                Console.WriteLine();
                Console.WriteLine("Product was not found!");
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }
    public async Task ShowAddOrderMenu()
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
                var existingCustomer = await _customerManagementService.GetCustomerWithCustomerNumber(Console.ReadLine()!);

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

                    Console.WriteLine("-------------");
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

                            bool savedOrder = await _orderPaymentManagementService.CreateOrder(newOrder);
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

    public async Task ShowAllOrdersMenu()
    {
        try
        {
            while (true)
            {
                ShowMenuText("Order list");
                Console.WriteLine("--------------\n");

                var orderList = await _orderPaymentManagementService.GetAllOrders();
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
                    }
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
    public async Task ShowOrderSearchMenu()
    {
        try
        {
            ShowMenuText("Search order");
            Console.WriteLine("----------------\n");
            Console.Write("Search order with order number: ");
            int.TryParse(Console.ReadLine(), out int userInput);
            var existingOrder = await _orderPaymentManagementService.GetOneOrder(userInput);

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
    public async Task ShowOrderUpdateMenu()
    {
        try
        {
            while (true)
            {
                ShowMenuText("Update order");
                Console.WriteLine("----------------\n");
                Console.Write("Order number: ");
                int.TryParse(Console.ReadLine()!, out int userInput);
                var orderToBeUpdated = await _orderPaymentManagementService.GetOneOrder(userInput);
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
                        Console.Write("Payment Method: ");
                        var validatedPaymentmethod = Console.ReadLine()!;
                        orderToBeUpdated.PaymentMethodName = validatedPaymentmethod;

                        Console.Write("Description: ");
                        var validatedDescription = Console.ReadLine()!;
                        orderToBeUpdated.Description = validatedDescription;

                        await _orderPaymentManagementService.UpdateOrder(orderToBeUpdated, userInput);

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
    public async Task ShowRemoveOrderMenu()
    {
        try
        {
            while (true)
            {
                ShowMenuText("Remove order");
                Console.WriteLine("-----------------\n");
                Console.Write("Remove order with order number: ");
                int.TryParse(Console.ReadLine()!, out int orderRemoveInput);
                var orderToRemoveFound = await _orderPaymentManagementService.GetOneOrder(orderRemoveInput);

                if (orderToRemoveFound != null)
                {
                    Console.WriteLine($"Order date: {orderToRemoveFound.OrderDate}\nCustomer number: {orderToRemoveFound.CustomerNumber}\nFirst name: {orderToRemoveFound.FirstName}\nLast name: {orderToRemoveFound.LastName}\nEmail: {orderToRemoveFound.Email}");
                    Console.WriteLine();
                    Console.Write("Do you want to remove? (y/n): ");
                    string orderRemoveInputAnswer = ValidateText(Console.ReadLine()!);
                    Thread.Sleep(500);
                    Console.Clear();
                    if (orderRemoveInputAnswer == "y")
                    {
                        var removeCustomer = await _orderPaymentManagementService.DeleteOrder(orderRemoveInput);
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


    public async Task ShowAddCustomerMenu()
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

                var savedCustomer = await _customerManagementService.CreateCustomer(newCustomer);
                if (savedCustomer != true)
                {
                    Console.WriteLine();
                    Console.WriteLine("Customer already in the list");
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
    public async Task ShowAllCustomersMenu()
    {
        try
        {
            while (true)
            {
                ShowMenuText("Customer list");
                Console.WriteLine("-----------------\n");
                var customerList = await _customerManagementService.GetAllCustomers();
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
                    Console.WriteLine("The customer list is empty!");
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
    public async Task ShowCustomerSearchMenu()
    {
        try
        {
            ShowMenuText("Search customer");
            Console.WriteLine("----------------\n");
            Console.WriteLine("[1] Search with email");
            Console.WriteLine("[2] Search with customer number");
            int.TryParse(Console.ReadLine(), out var result);

            switch (result)
            {
                case 1:
                    Console.Write("Search customer with email: ");
                    var customerFound = await _customerManagementService.GetOneCustomer(ValidateEmail(Console.ReadLine()!));
                    Thread.Sleep(500);
                    Console.Clear();
                    if (customerFound != null)
                    {
                        ShowMenuText("Customer was found");
                        Console.WriteLine("-------------------");
                        Console.WriteLine($"First name: {customerFound.FirstName}\n" +
                                          $"Last name: {customerFound.LastName}\n" +
                                          $"Email: {customerFound.Email}\n" +
                                          $"Street name: {customerFound.StreetName}\n" +
                                          $"City: {customerFound.City}\n" +
                                          $"Postal code: {customerFound.PostalCode}\n" +
                                          $"Country: {customerFound.Country}\n" +
                                          $"Role name: {customerFound.RoleName}\n" +
                                          "\n-------------------\n");
                    }
                    else
                    {
                        ShowMenuText("Customer was not found");
                        Console.WriteLine("------------------------");
                    }
                    break;
                case 2:
                    Console.Write("Search customer with customer number: ");
                    var customer = await _customerManagementService.GetCustomerWithCustomerNumber(Console.ReadLine()!);
                    Thread.Sleep(500);
                    Console.Clear();
                    if (customer != null)
                    {
                        ShowMenuText("Customer was found");
                        Console.WriteLine("-------------------");
                        Console.WriteLine($"First name: {customer.FirstName}\n" +
                                          $"Last name: {customer.LastName}\n" +
                                          $"Email: {customer.Email}\n" +
                                          $"Role name: {customer.RoleName}\n" +
                                          "\n-------------------\n");
                    }
                    else
                    {
                        ShowMenuText("Customer was not found");
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
    public async Task ShowCustomerUpdateMenu()
    {
        try
        {
            while (true)
            {
                ShowMenuText("Update customer");
                Console.WriteLine("----------------\n");
                Console.Write("Update customer with email: ");
                string customerUpdateEmailInput = ValidateEmail(Console.ReadLine()!);
                var customerToBeUpdated = await _customerManagementService.GetOneCustomer(customerUpdateEmailInput);
                Thread.Sleep(500);
                Console.Clear();

                if (customerToBeUpdated != null)
                {
                    ShowMenuText("Customer was found");
                    Console.WriteLine("-------------------");
                    Console.WriteLine();
                    Console.WriteLine($"First name: {customerToBeUpdated.FirstName}\n" +
                                      $"Last name: {customerToBeUpdated.LastName}\n" +
                                      $"Email: {customerToBeUpdated.Email}\n" +
                                      $"Street name: {customerToBeUpdated.StreetName}\n" +
                                      $"City: {customerToBeUpdated.City}\n" +
                                      $"Postal code: {customerToBeUpdated.PostalCode}\n" +
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

                        await _customerManagementService.UpdateCustomer(customerToBeUpdated, customerUpdateEmailInput);

                        ShowMenuText("Customer was updated");
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
    public async Task ShowRemoveCustomerMenu()
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
                    var customerToRemoveFound = await _customerManagementService.GetOneCustomer(customerRemoveInput);

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
                            await _customerManagementService.DeleteCustomer(customerRemoveInput);
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
                    var customerToRemove = await _customerManagementService.GetCustomerWithCustomerNumber(userRemoveInput);

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
                            await _customerManagementService.DeleteCustomer(customerToRemove.Email);
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
    public static string ValidateAll(string value)
    {
        if (!string.IsNullOrWhiteSpace(value))
        {
            return value.ToLower();
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
