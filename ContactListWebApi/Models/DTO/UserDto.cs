namespace ContactListWebApi.Models.DTO;

public class UserDto
{
    public string Login { get; set; } = string.Empty;
    public RoleDto? Role { get; set; }
}
