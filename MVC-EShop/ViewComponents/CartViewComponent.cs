using MVC_EShop.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using MVC_EShop.Models;
using Newtonsoft.Json;

namespace MVC_EShop.ViewComponents;

public class CartViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var rawJson = HttpContext.Session.GetString("Cart");
        var cart = rawJson is null ? new List<Product>() : JsonConvert.DeserializeObject<List<Product>>(rawJson);

        var cartItems = new List<CartItem>();

        Dictionary<int, CartItem> productsDictionary = new();
        if (cart is not null && cart.Any())
        {

            foreach (var product in cart)
            {
                if (productsDictionary.ContainsKey(product.Id))
                {
                    productsDictionary[product.Id].Amount++;
                }
                else
                {
                    productsDictionary.Add(product.Id, new CartItem(product, 1));

                }
            }
            cartItems = productsDictionary.Values.ToList();

        }
        return View(cartItems);
    }
}
