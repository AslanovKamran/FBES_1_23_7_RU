namespace ContactListWebApi.Models;

public class RefreshToken
{
    public int Id { get; set; }
    public string Token { get; set; } = string.Empty;
    public DateTime ExpriresAt { get; set; }
    public int UserId { get; set; }

    //Navigation Property
    public User? User { get; set; }
}
