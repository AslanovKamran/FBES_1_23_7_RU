using SalaryCalculator.Application;

namespace SalaryCalculator.Tests;

public class CalculatorTests
{
    [Fact]
    public void Add_SimpleValues_ReturnsCorrectResult()
    {
        // Arrange
        double expected = 8;
        double x = 5;
        double y = 3;

        // Act
        double result = Calculator.Add(x, y);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(1,2,3)]
    [InlineData(10.5,1.6,12.1)]
    [InlineData(20,20,40)]
    public void Add_ShouldReturnCorrectResult_When_Multiple_Data_Given(double x, double y, double expected)
    {
        // Arrange
       

        // Act
        double actual = Calculator.Add(x, y);

        // Assert
        Assert.Equal(actual, expected);
    }

    [Theory]
    [InlineData(8,4,2)]
    public void Divide_SimpleValues_ReturnsCorrectResult(double x, double y, double expected)
    {
        // Arrange

        // Act
        double result = Calculator.Divide(x, y);
        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Divide_DivideByZero()
    {
        // Arrange
        double expected = 0;
        // Act
        double result = Calculator.Divide(15, 0);
        // Assert
        Assert.Equal(expected, result);
    }
}
