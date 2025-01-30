using Microsoft.AspNetCore.Mvc;

namespace FirstWebApp_29_01_2025_.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Contacts()
    {
        ViewBag.Contacts = new List<string>
        {
            "John", "Bob", "Cody","Max"
        };
        return View();
    }
}
