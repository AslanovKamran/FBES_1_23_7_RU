using Humanizer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesApp.Areas.Identity.Data;
using RazorPagesApp.Data;
using RazorPagesApp.Models;

namespace RazorPagesApp.Pages;

public class GuestBookModel : PageModel
{
    [BindProperty]
    public string Text { get; set; } = string.Empty;
    public List<Comment> Comments{ get; set; } 


    private readonly RazorPagesAppContext _context;
    private readonly UserManager<AppUser> _userManager;

    public GuestBookModel(RazorPagesAppContext context, UserManager<AppUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }


    public async Task<IActionResult> OnPostAsync()
    {
        if (User.Identity.IsAuthenticated)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            _context.Comments.Add(new Comment
            {
                Text = Text,
                Date = DateTime.Now,
                AppUserId = user.Id
            });
            await _context.SaveChangesAsync();
        }
        return RedirectToPage();
    }

    public IActionResult OnGet() 
    {
        Comments = _context.Comments
            .Include(c => c.AppUser)
            .ToList();

        return Page();
    }
}
