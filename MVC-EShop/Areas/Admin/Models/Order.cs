namespace MVC_EShop.Areas.Admin.Models;

public class Order
{
    public int Id { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Note { get; set; }
    public bool IsDone { get; set; }

    //Добавить то же самое в Product
    public List<OrderProduct> OrderProducts { get; } = [];
}

