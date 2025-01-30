using System.Linq.Expressions;
using FirstWebApp_29_01_2025_.Models;
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

    public IActionResult Search(Example expression)
    {
        var result = expression.Operation switch
        {
            '+' => expression.First + expression.Second,
            '-' => expression.First - expression.Second,
            '*' => expression.First * expression.Second,
            '/' => expression.Second == 0 ? 0 : expression.First / expression.Second,
            _ => 0
        };

        ViewBag.Result = result;
        return View();
    }

    public IActionResult Error(int code)
    {
        ViewBag.Code = code;
        return View();
    }
}
