using Microsoft.EntityFrameworkCore;
using MVC_EShop.Areas.Admin.Models;

namespace MVC_EShop.Areas.Admin.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    // MS SQL Tables
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
}
