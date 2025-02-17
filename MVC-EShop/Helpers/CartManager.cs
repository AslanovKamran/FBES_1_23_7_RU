using MVC_EShop.Areas.Admin.Models;
using Newtonsoft.Json;

namespace MVC_EShop.Helpers;

public static class CartManager
{
    public static void AddToCart(ISession session, Product product)
    {
        List<Product> cart;
        try
        {
            var rawProducts = session.GetString("Cart");
            cart = JsonConvert.DeserializeObject<List<Product>>(rawProducts!)!;

        }
        catch (Exception)
        {
            cart = new();
        }
        cart.Add(product);
        session.SetString("Cart", JsonConvert.SerializeObject(cart));
    }
}
