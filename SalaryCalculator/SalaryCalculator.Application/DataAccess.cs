using SalaryCalculator.Application.Models;

namespace SalaryCalculator.Application;

public static class DataAccess
{
    private static string personTextFile = @"C:\Users\DELL\Desktop\Test.txt";

    public static void AddNewPerson(PersonModel person)
    {

        File.AppendAllText(personTextFile, $"{person.FirstName},{person.LastName}\n");
    }

    public static List<PersonModel> GetAllPeople()
    {
        var output = new List<PersonModel>();
        string[] content = File.ReadAllLines(personTextFile);

        foreach (var line in content)
        {
            var data = line.Split(',');
            output.Add(new PersonModel()
            {
                FirstName = data[0],
                LastName = data[1]
            });
        }
        return output;
    }
}
