namespace SalaryCalculator.Application;

public static class Examples
{
    public static string ExampleOfTextFile(string filePath) 
    {
        return  File.Exists(filePath) 
            ? File.ReadAllText(filePath)
            : throw new FileNotFoundException();
    }
}
