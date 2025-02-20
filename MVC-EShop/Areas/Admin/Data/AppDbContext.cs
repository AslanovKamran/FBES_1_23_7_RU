using Microsoft.EntityFrameworkCore;
using MVC_EShop.Areas.Admin.Models;

namespace MVC_EShop.Areas.Admin.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OrderProduct>()
            .HasKey(op => new { op.ProductId, op.OrderId });

        modelBuilder.Entity<OrderProduct>()
           .HasOne(p => p.Product)
           .WithMany(p => p.OrderProducts)
           .HasForeignKey(p => p.ProductId);


        modelBuilder.Entity<OrderProduct>()
            .HasOne(o => o.Order)
            .WithMany(o => o.OrderProducts)
            .HasForeignKey(o => o.OrderId);
    }

    // MS SQL Tables
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderProduct> OrderProducts { get; set; }
}
