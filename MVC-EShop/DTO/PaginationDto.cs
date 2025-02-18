namespace MVC_EShop.DTO;

public class PaginationDto
{
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int ItemsPerPage { get; set; }
    public int TotalItems { get; set; }
}
