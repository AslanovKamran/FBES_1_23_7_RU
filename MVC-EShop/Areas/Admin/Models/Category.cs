namespace MVC_EShop.Areas.Admin.Models;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }

    //Nav Property 
    List<Product> Products { get; set; } = new();
}
