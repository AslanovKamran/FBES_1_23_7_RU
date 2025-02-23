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

    public static void RemoveFromCart(ISession session, int productId)
    {
        var rawProducts = session.GetString("Cart");
        if (rawProducts is null) return;

        var cart = JsonConvert.DeserializeObject<List<Product>>(rawProducts)!;

        // Remove only ONE occurrence of the product
        var productToRemove = cart.FirstOrDefault(p => p.Id == productId);
        if (productToRemove != null)
        {
            cart.Remove(productToRemove);
            session.SetString("Cart", JsonConvert.SerializeObject(cart));
        }
    }

    public static void RemoveAllFromCart(ISession session, int productId)
    {
        var rawProducts = session.GetString("Cart");
        if (rawProducts is null) return;

        var cart = JsonConvert.DeserializeObject<List<Product>>(rawProducts)!;

        // Remove ALL occurrences of the product
        cart.RemoveAll(p => p.Id == productId);

        session.SetString("Cart", JsonConvert.SerializeObject(cart));
    }

    public static IEnumerable<Product> GetProducts(ISession session)
    {
        var rawJson = session.GetString("Cart");
        if (rawJson is null) return Enumerable.Empty<Product>();

        var cart = JsonConvert.DeserializeObject<List<Product>>(rawJson);
        return cart ?? Enumerable.Empty<Product>();
    }

    public static void FlushCart(ISession session) => session.Remove("Cart");
}
