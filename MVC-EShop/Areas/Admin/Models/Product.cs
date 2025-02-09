using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_EShop.Areas.Admin.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }

    [Column(TypeName = "decimal(9,2)")]
    public decimal Price { get; set; }
  
    public string ImageUrl { get; set; }    
    public string Description { get; set; } 
    public int CategoryId { get; set; }

    //Nav property
    public Category? Category { get; set; }
    // Add this to handle the uploaded file
    [NotMapped] // This tells EF Core to ignore this property when mapping the entity
    public IFormFile ImageFile { get; set; } // Used for binding the uploaded image
}