using System.ComponentModel.DataAnnotations;

namespace AspNetDBPortal.Models;

public class Post
{
    [Key]
    public int Id { get; set; }
    
    [Required(AllowEmptyStrings = false,ErrorMessage ="Zapolni title")]
    [MaxLength(100, ErrorMessage ="Ochen bolshoi title")]
    [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage ="Tolko bukvi plz")]
    public string Title { get; set; } = string.Empty;

    [Required(AllowEmptyStrings = false)]
    [MaxLength(200)]
    public string Content { get; set; } = string.Empty;
    
    public string? Description { get; set; } = string.Empty;
    
    public string? ImageUrl { get; set; } = string.Empty;
    
    public DateTime CreatedAt { get; set; }

    //Auto set
    public Post() => CreatedAt = DateTime.Now;
}

