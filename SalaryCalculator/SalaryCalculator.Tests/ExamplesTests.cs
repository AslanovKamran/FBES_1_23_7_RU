using SalaryCalculator.Application;

namespace SalaryCalculator.Tests;

public class ExamplesTests
{
    [Fact]
    public void ExampleOfTextFile_Valid_FilePath_Should_Work() 
    {
        //Arrange 
        const string FilePath = @"C:\Users\DELL\Desktop\Test.txt";
        const string Expected = "Hello, world!";

        //Act
        var actual = Examples.ExampleOfTextFile(FilePath);

        //Assert
        Assert.Equal(Expected, actual);
    }

    [Fact]
    public void ExampleOfTextFile_Invalid_FilePath_Should_Fail()
    {
        //Arrange 
        const string FilePath = @"C:\Users\DELL\Desktop\WrongFilePath.txt";
        
        //Assert
        Assert.Throws<FileNotFoundException>(()=>Examples.ExampleOfTextFile(FilePath));
    }
}
