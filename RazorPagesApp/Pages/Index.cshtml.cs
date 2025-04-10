using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPagesApp.Pages;

public class IndexModel : PageModel
{
    public string Name { get; set; } = "Kamran";
    [BindProperty(SupportsGet = true)]
    public string Surname { get; set; } = "Aslanov";
    public void OnGet()
    {
    }
}
 