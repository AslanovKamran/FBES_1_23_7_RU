using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_EShop.Areas.Admin.Data;
using MVC_EShop.DTO;
using MVC_EShop.Helpers;
using MVC_EShop.Models;

namespace MVC_EShop.Controllers
{
    public class HomeController : Controller
    {
        #region Initialization

        private readonly AppDbContext _context;
        public HomeController(AppDbContext context) => _context = context;

        #endregion

        #region Get

        public IActionResult Index(int? categoryId)
        {

            var products = categoryId is null 
                                         ? _context.Products 
                                         : _context.Products.Where(x => x.CategoryId == categoryId);

            var categories = _context.Categories;

            var productsCategoriesDto = new ProductsCategoriesDto
            {
                Products = products.ToList(),
                Categories = categories.ToList()
            };

            return View(productsCategoriesDto);
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

        #endregion

        #region Cart

        public IActionResult AddToCart(int id) 
        {
            var productToAdd = _context.Products.FirstOrDefault(p => p.Id == id);
            CartManager.AddToCart(HttpContext.Session, productToAdd);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult RemoveFromCart(int productId)
        {
            CartManager.RemoveFromCart(HttpContext.Session, productId);
            return RedirectToAction("Index", "Home", new { cartOpened = true });
        }

        public IActionResult RemoveAllFromCart(int productId)
        {
            CartManager.RemoveAllFromCart(HttpContext.Session, productId);
            return RedirectToAction("Index", "Home", new { cartOpened = true });
        }

        #endregion

        #region Error Page

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #endregion
    }
}
