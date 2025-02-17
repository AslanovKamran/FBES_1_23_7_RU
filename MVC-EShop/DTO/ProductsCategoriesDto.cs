using MVC_EShop.Areas.Admin.Models;

namespace MVC_EShop.DTO;

public class ProductsCategoriesDto
{
    public List<Product> Products { get; set; } = new();
    public List<Category> Categories{ get; set; } = new();
}
