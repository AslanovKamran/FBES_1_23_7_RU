using Microsoft.EntityFrameworkCore;
using MVC_EShop.Areas.Admin.Data;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using MVC_EShop.Helpers;
using MVC_EShop.Models;
using MVC_EShop.DTO;
using Microsoft.AspNetCore.Authorization;

namespace MVC_EShop.Controllers;

public class HomeController : Controller
{
    const int ItemsPerPage = 4;

    #region Initialization

    private readonly AppDbContext _context;
    public HomeController(AppDbContext context) => _context = context;

    #endregion

    #region Get

    public IActionResult Index(int? categoryId, int page = 1)
    {
        var query = (categoryId is null) ? _context.Products.AsQueryable()
                                         : _context.Products.AsQueryable().Where(p => p.CategoryId == categoryId);

        int totalItems = query.Count();
        int totalPages = (int)Math.Ceiling(totalItems / (double)ItemsPerPage);

        var products = query
            .Skip((page - 1) * ItemsPerPage)
            .Take(ItemsPerPage)
            .ToList();

        var response = new ProductsCategoriesDto
        {
            Products = products,
            Categories = _context.Categories.ToList(),
            Pagination = new PaginationDto
            {
                CurrentPage = page,
                TotalPages = totalPages,
                ItemsPerPage = ItemsPerPage,
                TotalItems = totalItems
            }
        };

        ViewBag.SelectedCategoryId = categoryId;

        return View(response);
    }

    public IActionResult ProductDetails(int id)
    {
        var product = _context.Products
                              .Include(p => p.Category)
                              .FirstOrDefault(p => p.Id == id);

        if (product is null)
            return BadRequest();

        var selectedCategoryId = product.CategoryId;
        var relatedProducts = _context.Products
                                      .Where(p => p.CategoryId == selectedCategoryId && p.Id != id)
                                      .ToList();

        ViewBag.RelatedProducts = relatedProducts;

        return View(product);
    }

    [Authorize]
    public IActionResult SecretPage() => View();

    #endregion

    #region Cart

    public IActionResult AddToCart(int id)
    {
        var productToAdd = _context.Products.FirstOrDefault(p => p.Id == id);

        if (productToAdd is null)
            return BadRequest();

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
