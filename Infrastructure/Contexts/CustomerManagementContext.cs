using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts;

public partial class CustomerManagementContext(DbContextOptions<CustomerManagementContext> options) : DbContext(options)
{

    public virtual DbSet<CustomerEntity> Customers { get; set; }
    public virtual DbSet<RoleEntity> Roles { get; set; }
    public virtual DbSet<AddressEntity> Addresses { get; set; }
    public virtual DbSet<OrderEntity> Orders { get; set; }
    public virtual DbSet<PaymentMethodEntity> PaymentMethods { get; set; }
    public virtual DbSet<CustomerAddressEntity> CustomerAddresses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<CustomerAddressEntity>()
               .HasKey(x => new { x.CustomerNumber, x.AddressId });
    }
}
