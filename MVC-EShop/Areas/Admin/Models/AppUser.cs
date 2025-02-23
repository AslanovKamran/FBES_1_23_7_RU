using Microsoft.AspNetCore.Identity;

namespace MVC_EShop.Areas.Admin.Models;

public class AppUser : IdentityUser
{
    public string? FullName { get; set; } 
    public string? ImageUrl { get; set; } 
}
