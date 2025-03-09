using System.ComponentModel.DataAnnotations;

namespace ContactListWebApi.Models;

public class Role
{
    [Key]
    public int Id { get; set; }
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    public IEnumerable<User> Users { get; set; } = new List<User>();
}
