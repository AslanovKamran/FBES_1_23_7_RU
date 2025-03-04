using System.ComponentModel.DataAnnotations;

namespace ContactListWebApi.Models;

public class Contact
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MinLength(3)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [Phone]
    public string Phone { get; set; } = string.Empty;

    public Contact() { }

    public Contact(int id, string name, string phone)
    {
        Id = id;
        Name = name;
        Phone = phone;
    }
}
