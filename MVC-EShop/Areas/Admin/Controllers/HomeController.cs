using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_EShop.Areas.Admin.Data;

namespace MVC_EShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context) => _context = context;
        public IActionResult Index()
        {
            var productsCount = _context.Products.Count();
            var categoriesCount = _context.Categories.Count();

            ViewBag.ProductsCount = productsCount;
            ViewBag.CategoriesCount = categoriesCount;

            return View();
        }
    }
}
