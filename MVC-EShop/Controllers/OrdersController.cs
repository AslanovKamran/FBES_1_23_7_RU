using Microsoft.AspNetCore.Mvc;
using MVC_EShop.Areas.Admin.Data;
using MVC_EShop.Areas.Admin.Models;
using MVC_EShop.Helpers;

namespace MVC_EShop.Controllers;

public class OrdersController : Controller
{
    private readonly AppDbContext _context;
    public OrdersController(AppDbContext context) => _context = context;

    public IActionResult NewOrder()
    {

        return View();
    }

    [HttpPost]
    public IActionResult NewOrder(Order order)
    {
        var orderEntity = _context.Orders.Add(order).Entity;
        _context.SaveChanges(); // Save the new order to generate an OrderId

        var cartProducts = CartManager.GetProducts(HttpContext.Session)
            .GroupBy(p => p.Id)
            .Select(g => new { ProductId = g.Key, Quantity = g.Count() }) // Get product quantity
            .ToList();

        var existingOrderProducts = _context.OrderProducts
            .Where(op => op.OrderId == orderEntity.Id)
            .ToList(); // Load existing order products into memory to avoid multiple tracking

        foreach (var product in cartProducts)
        {
            var existingOrderProduct = existingOrderProducts
                .FirstOrDefault(op => op.ProductId == product.ProductId);

            if (existingOrderProduct != null)
            {
                // Update the quantity in the tracked entity
                existingOrderProduct.Quantity += product.Quantity;
            }
            else
            {
                // Add new entry
                _context.OrderProducts.Add(new OrderProduct
                {
                    ProductId = product.ProductId,
                    OrderId = orderEntity.Id,
                    Quantity = product.Quantity
                });
            }
        }
        _context.SaveChanges(); // Save all changes in one go
        return RedirectToAction("Index", "Home");
    }

}
