namespace ContactListWebApi.Models;

public class Contact
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;

    public Contact() { }

    public Contact(int id, string name, string phone)
    {
        Id = id;
        Name = name;
        Phone = phone;
    }
}
