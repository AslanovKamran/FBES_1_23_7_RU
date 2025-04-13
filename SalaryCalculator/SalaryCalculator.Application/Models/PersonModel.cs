namespace SalaryCalculator.Application.Models;

public class PersonModel
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    public string FullName
    {
        get
        {
            return $"{FirstName} {LastName}";
        }
    }
}
