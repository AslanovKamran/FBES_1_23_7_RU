using System.ComponentModel.DataAnnotations;

namespace MVC_EShop.Models.Dtos;

public class RegisterDto
{
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;
     
    [Required]
    [Compare("Password",ErrorMessage ="Passwords do not match")]
    public string ConfirmPassword { get; set; } = string.Empty;
}
