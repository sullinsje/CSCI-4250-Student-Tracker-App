using Microsoft.AspNetCore.Mvc;
using StudentTrackerApp.Services;
using StudentTrackerApp.Models.Entities;
using StudentTrackerApp.Models;
using Microsoft.AspNetCore.Identity;

namespace StudentTrackerApp.Controllers;

[Route("[controller]")]
public class AuthController : Controller
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager; 
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ApplicationDbContext _context; 

    public AuthController(
        SignInManager<ApplicationUser> signInManager,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        ApplicationDbContext context)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _roleManager = roleManager;
        _context = context; 
    }

    #region Get Login Page

    [HttpGet("login/student")]
    public IActionResult StudentLogin(string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        ViewData["UserRole"] = "Student";
        return View("Login"); 
    }

    [HttpGet("login/teacher")]
    public IActionResult TeacherLogin(string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        ViewData["UserRole"] = "Teacher";
        return View("Login"); 
    }

    [HttpGet("login/admin")]
    public IActionResult AdminLogin(string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        ViewData["UserRole"] = "Admin";
        return View("Login");
    }

    #endregion

    [HttpPost("login")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginModel model, string? returnUrl = null)
    {
        bool isPersistent = model.RememberMe;

        if (!ModelState.IsValid)
        {
            ViewBag.Error = "Invalid credentials format.";
            return View("Login", model); 
        }

        var result = await _signInManager.PasswordSignInAsync(
            model.Email,
            model.Password,
            isPersistent: isPersistent,
            lockoutOnFailure: true);

        if (result.Succeeded)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user != null)
            {

                if (await _userManager.IsInRoleAsync(user, "Admin"))
                    return RedirectToAction("Index", "Admin");
                else if (await _userManager.IsInRoleAsync(user, "Teacher"))
                    return RedirectToAction("Dashboard", "Teacher");
                else
                    return RedirectToAction("Dashboard", "Student");
            }

            return LocalRedirect(returnUrl ?? "/");
        }

        ViewBag.Error = "Invalid Student ID or Password.";
        return View("Login", model);
    }

    #region Register

    [HttpGet("register/{roleName}")]
    public IActionResult Register(string roleName)
    {
        if (roleName != "student" && roleName != "teacher" && roleName != "admin")
        {
            return NotFound();
        }

        var model = new RegisterModel { RoleName = roleName };
        ViewData["Title"] = $"{char.ToUpper(roleName[0]) + roleName.Substring(1)} Registration";

        return View("Register", model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> PerformRegister(RegisterModel model)
    {
        if (ModelState.IsValid)
        {
            var user = new ApplicationUser 
            { 
                UserName = model.Email, 
                Email = model.Email,
                Name = model.Name 
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                string role = model.RoleName.ToLower();

                if (await _roleManager.RoleExistsAsync(role))
                {
                    await _userManager.AddToRoleAsync(user, role);

                    if (role == "student")
                    {
                        var student = new Student
                        {
                            Name = model.Name, 
                            UserId = user.Id
                        };

                        _context.Students.Add(student);
                        await _context.SaveChangesAsync();
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, $"Role '{model.RoleName}' does not exist. Please contact support.");
                    await _userManager.DeleteAsync(user); 
                    return View("Register", model);
                }

                await _signInManager.SignInAsync(user, isPersistent: false);

                if (role == "admin")
                    return RedirectToAction("Index", "Admin");
                else if (role == "teacher")
                    return RedirectToAction("Dashboard", "Teacher");
                else
                    return RedirectToAction("Dashboard", "Student");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        return View("Register", model);
    }
    
    public IActionResult Logout()
    {
        return View();
    }

    [HttpPost("logout")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> PerformLogout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }


    #endregion
}