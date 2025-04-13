using SalaryCalculator.Application;
using SalaryCalculator.Application.Models;

var newPerson = new PersonModel()
{
    FirstName = "Josh",
    LastName = "Peck"
};

DataAccess.AddNewPerson(newPerson);
var people = DataAccess.GetAllPeople();
foreach (var item in people)
{
    Console.WriteLine(item.FullName);
}