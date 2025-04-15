using MyApp.Application.Domain.Models;
using MyApp.Application.Repositories;
using Newtonsoft.Json;

namespace MyApp.Application.Services;

public interface IUserService 
{

}

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public string GetUserFullName(int id)
    {
        var user = _userRepository.GetUserById(id);
        return $"{user.FirstName} {user.LastName}";
    }

    public async Task<string> GetUserFullNameAsync(int id) 
    {
        var user = await _userRepository.GetUserByIdAsync(id);
        return $"{user.FirstName} {user.LastName}";
    }

    public async Task<List<User>> GetUsersFromJsonFileAsync(string filePath)
    {
        var jsonData = await File.ReadAllTextAsync(filePath);
        var users = JsonConvert.DeserializeObject<List<User>>(jsonData);
        return users ?? [];
    }
}
