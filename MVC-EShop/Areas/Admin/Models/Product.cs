using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_EShop.Areas.Admin.Models;

public class Product
{

    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    [Column(TypeName = "decimal(9,2)")]
    public decimal Price { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int CategoryId { get; set; }
    public int Rating { get; set; }
    //Nav property
    public Category? Category { get; set; }

    [NotMapped]
    public IFormFile ImageFile { get; set; }

    public List<OrderProduct> OrderProducts { get; } = [];
}