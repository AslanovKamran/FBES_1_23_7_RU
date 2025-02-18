 using MVC_EShop.Areas.Admin.Models;

namespace MVC_EShop.Models;

public class CartItem
{
    public Product Product{ get; set; }
    public int Amount{ get; set; }

    public CartItem(Product product, int amount)
    {
        Product = product;
        Amount = amount;
    }
}
