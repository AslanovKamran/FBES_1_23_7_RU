namespace AspNetOmdDb.Models;

public class ApiResult
{
    public List<Movie> Search { get; set; } = new();
    public string totalResults { get; set; } = string.Empty;
    public string Response { get; set; } = string.Empty;
}
