using System.ComponentModel.DataAnnotations;

namespace ContactListWebApi.Requests.AuthorizationRequests;

public class RegisterRequest
{
    [Required(AllowEmptyStrings = false)]
    public required string Login { get; set; }
    [Required(AllowEmptyStrings = false)]
    public required string Password { get; set; }
    [Required]
    public int RoleId { get; set; }
}
