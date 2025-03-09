using System.ComponentModel.DataAnnotations;

namespace ContactListWebApi.Requests.AuthorizationRequests;

public class LoginRequest
{
    [Required(AllowEmptyStrings = false)]
    public required string Login { get; set; }
    [Required(AllowEmptyStrings = false)]
    public required string Password { get; set; }
}
