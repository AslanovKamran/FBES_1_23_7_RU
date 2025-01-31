using Microsoft.EntityFrameworkCore;

namespace AspNetDBPortal.Models;

public class NewsPortalDbContext : DbContext
{
    public NewsPortalDbContext(DbContextOptions options)
        : base(options) { }

    public DbSet<Post> Posts { get; set; }

}
