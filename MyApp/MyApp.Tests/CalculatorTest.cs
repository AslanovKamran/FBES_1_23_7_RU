using MyApp.Console;

namespace MyApp.Tests;

public class CalculatorTest
{
    [Fact]
    public void Add_Two_Numbers_ReturnsSum() 
    {
        //Arrange
        var calculator = new Calculator();
        //Act
        var result = calculator.Add(2, 3);
        //Assert
        Assert.Equal(5, result);
    }

    [Fact]
    public void Subtract_Two_Numbers_ReturnsDifference()
    {
        //Arrange
        var calculator = new Calculator();
        //Act
        var result = calculator.Subtract(10, 3);
        //Assert
        Assert.Equal(7, result);
    }

    [Fact]
    public void Division_ByZero_ThrowsException()
    {
        //Arrange
        var calculator = new Calculator();
        //Act & Assert
        Assert.Throws<DivideByZeroException>(() => calculator.Divide(10, 0));
    }

    //Assert Types
    //Assert.Equal(expected, actual)
    //Assert.NotEqual(expected, actual)
    //Assert.True(condition)
    //Assert.False(condition)
    //Assert.Null(object)
    //Assert.NotNull(object)
    //Assert.Throws<TException>(() => ...)

    [Theory]
    [InlineData(1,2,3)]
    [InlineData(5,10,15)]
    [InlineData(-5,10,5)]
    public void Add_Two_Numbers_ReturnsCorrectSum(int a, int b, int sum) 
    {
        //Arrange
        var calculator = new Calculator();
        //Act
        var result = calculator.Add(a, b);
        //Assert
        Assert.Equal(sum, result);
    }
}
