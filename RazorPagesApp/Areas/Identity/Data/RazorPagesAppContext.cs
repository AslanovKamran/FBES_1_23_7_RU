using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RazorPagesApp.Areas.Identity.Data;
using RazorPagesApp.Models;

namespace RazorPagesApp.Data;

public class RazorPagesAppContext : IdentityDbContext<AppUser>
{
    public RazorPagesAppContext(DbContextOptions<RazorPagesAppContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
    public DbSet<Comment> Comments { get; set; }
}
