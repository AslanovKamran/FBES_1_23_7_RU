using Moq;
using MyApp.Application.Domain.Models;
using MyApp.Application.Repositories;
using MyApp.Application.Services;

namespace MyApp.Tests.UserServiceTest;

public class UserServiceTests
{
    [Fact]
    public void GetUserFullName_ReturnsCorrectFullName()
    {
        //Arrange
        var mockRepository = new Mock<IUserRepository>();
        mockRepository.Setup(repo => repo.GetUserById(1)).Returns(new User { FirstName = "John", LastName = "Doe" });

        var service = new UserService(mockRepository.Object);

        //Act
        var result = service.GetUserFullName(1);

        var expected = "John Doe";
        //Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public async Task GetUserFullNameAsync_ReturnsCorrectFullName()
    {
        //Arrange
        var mockRepository = new Mock<IUserRepository>();
        mockRepository.Setup(repo => repo.GetUserByIdAsync(1)).ReturnsAsync(new User { FirstName = "John", LastName = "Doe" });

        var service = new UserService(mockRepository.Object);

        //Act
        var result = await service.GetUserFullNameAsync(1);

        var expected = "John Doe";
        //Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public async Task GetUsersFromJsonFileAsync_ReturnsCorrectUsers()
    {
        //Arrange
        var filePath = @"D:\VS\FBES_1_23_7_RU\MyApp\test.json";

        // Mocking the repository is not needed for this test
        var userService = new UserService(null!);

        //Act
        var users = await userService.GetUsersFromJsonFileAsync(filePath);

        //Assert
        Assert.NotNull(users);
        Assert.Equal(2, users?.Count);
        Assert.Equal("John", users?[0].FirstName);
        Assert.Equal("Doe", users?[0].LastName);
        Assert.Equal("Jane", users?[1].FirstName);
        Assert.NotEqual("Doe", users?[1].LastName);
    }
}
