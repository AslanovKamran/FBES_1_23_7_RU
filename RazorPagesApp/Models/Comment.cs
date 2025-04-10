using RazorPagesApp.Areas.Identity.Data;

namespace RazorPagesApp.Models;

public class Comment
{
    public int Id { get; set; }
    public string Text { get; set; }
    public DateTime Date { get; set; }

    public string? AppUserId{ get; set; }

    //Navigation property
    public AppUser? AppUser{ get; set; }
}
