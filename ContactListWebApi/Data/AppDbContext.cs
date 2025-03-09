using ContactListWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactListWebApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Contact> Contacts { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles{ get; set; }
}
