using System.ComponentModel.DataAnnotations;

namespace ContactListWebApi.Requests.AuthorizationRequests;

public class RefreshTokenRequest
{
    [Required]
    public required string RefreshToken { get; set; }
}
