using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC_EShop.Areas.Admin.Models;
using MVC_EShop.Models.Dtos;
using System.Threading.Tasks;

namespace MVC_EShop.Controllers;

public class AccountController : Controller
{

    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;

    public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    #region Register

    [HttpGet]
    public IActionResult Register() => View();

    //Id - a77e4fc7-b7be-4445-b88d-d1320f895137 Pswr - u4g98Aj'm<
    [HttpPost]
    public async Task<IActionResult> Register(RegisterDto register)
    {
        if (!ModelState.IsValid)
            RedirectToRegisterWithError("Invalid Model State");

        var appUser = new AppUser
        {
            UserName = register.Email,
            Email = register.Email
        };

        var registerResult = await _userManager.CreateAsync(appUser, register.Password);

        if (!registerResult.Succeeded)
            RedirectToRegisterWithError("Error while saving the user");

        TempData["Success"] = "User registered successfully";
        return RedirectToAction("Index", "Home");
    }

    // Helper method to handle register failures
    private IActionResult RedirectToRegisterWithError(string errorMessage)
    {
        TempData["Error"] = errorMessage;
        return RedirectToAction(nameof(Register));
    }

    #endregion

    #region Login

    [HttpGet]
    public IActionResult Login() => View();

    [HttpPost]
    public async Task<IActionResult> Login(LoginDto login)
    {
        if (!ModelState.IsValid)
            return RedirectToLoginWithError("Invalid Model State");

        var appUser = await _userManager.FindByEmailAsync(login.Email);

        if (appUser == null)
            return RedirectToLoginWithError("User not found!");

        if (!await _userManager.CheckPasswordAsync(appUser, login.Password))
            return RedirectToLoginWithError("Invalid Password");

        await _signInManager.SignInAsync(appUser, true);
        TempData["Success"] = "User logged in successfully";
        return RedirectToAction("Index", "Home");
    }

    // Helper method to handle login failures
    private IActionResult RedirectToLoginWithError(string errorMessage)
    {
        TempData["Error"] = errorMessage;
        return RedirectToAction(nameof(Login));
    }

    #endregion

    #region Logout
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();

        TempData["Info"] = "User logged out successfully";
        return RedirectToAction("Index", "Home");
    }
    #endregion
}
