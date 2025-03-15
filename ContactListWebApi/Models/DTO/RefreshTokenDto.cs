namespace ContactListWebApi.Models.DTO;

public class RefreshTokenDto
{
    public string Token { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
    public UserDto? User { get; set; }
}
