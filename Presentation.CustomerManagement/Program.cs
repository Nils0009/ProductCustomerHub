using Infrastructure.Contexts;
using Infrastructure.Dtos;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Presentation.CustomerManagement.Services;

var builder = Host.CreateDefaultBuilder().ConfigureServices(services =>
{
    services.AddDbContext<CustomerManagementContext>(x => x.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Projects\\ProductCustomerHub\\Infrastructure\\Data\\CustomerManagement.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=True"));
    services.AddScoped<AddressRepository>();
    services.AddScoped<CustomerRepository>();
    services.AddScoped<CustomerAddressRepository>();
    services.AddScoped<OrderRepository>();
    services.AddScoped<PaymentMethodRepository>();
    services.AddScoped<RoleRepository>();
    services.AddScoped<CustomerManagementService>();
    services.AddScoped<OrderPaymentManagementService>();
    services.AddSingleton<MenuService>();

}).Build();

builder.Start();
Console.Clear();

var menuService = builder.Services.GetRequiredService<MenuService>();
menuService.ShowMainMenu();

