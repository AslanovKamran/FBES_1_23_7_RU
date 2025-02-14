using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_EShop.Areas.Admin.Data;
using MVC_EShop.Models;

namespace MVC_EShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        public HomeController(AppDbContext context) => _context = context;

        public IActionResult Index(int? categoryId)
        {


            var products = categoryId is null 
                                         ? _context.Products 
                                         : _context.Products.Where(x => x.CategoryId == categoryId);

            var categories = _context.Categories;
            ViewBag.Categories = categories;
            return View(products.ToList());
        }

        public IActionResult ProductDetails(int id) 
        {
            var product = _context.Products.Include(p=>p.Category).FirstOrDefault(p => p.Id == id);

            if (product is null) 
                return BadRequest();

            var selectedCategoryId = product.CategoryId;
            var relatedProducts = _context.Products.Where(p => p.CategoryId == selectedCategoryId && p.Id != id).ToList();
            ViewBag.RelatedProducts = relatedProducts;

            return View(product);
        }
      
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
