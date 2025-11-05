using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using StudentTrackerApp.Services;
using StudentTrackerApp.Models.Entities;
using StudentTrackerApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace StudentTrackerApp.Controllers;

[Route("[controller]")]
public class AuthController : Controller
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager; // Often useful too
    private readonly RoleManager<IdentityRole> _roleManager; // For role checks

    // Dependency Injection via constructor
    public AuthController(
        SignInManager<ApplicationUser> signInManager,
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    #region Get Login Page

    [HttpGet("login/student")]
    public IActionResult StudentLogin(string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        ViewData["UserRole"] = "Student"; // Used for view logic or messaging
        return View("Login"); // Load the generic Login.cshtml view
    }

    [HttpGet("login/teacher")]
    public IActionResult TeacherLogin(string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        ViewData["UserRole"] = "Teacher";
        return View("Login"); // Load the generic Login.cshtml view
    }

    [HttpGet("login/admin")]
    public IActionResult AdminLogin(string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        ViewData["UserRole"] = "Admin";
        return View("Login"); // Load the generic Login.cshtml view
    }

    #endregion

    [HttpPost("login")]
    [ValidateAntiForgeryToken] 
    public async Task<IActionResult> Login(LoginModel model, string? returnUrl = null)
    {
        // Add checks for the Remember Me checkbox data
        bool isPersistent = model.RememberMe; // Assuming you add RememberMe to LoginModel

        if (!ModelState.IsValid)
        {
            // If validation fails, return to the view with errors
            ViewBag.Error = "Invalid credentials format.";
            return View(model); // Assumes you're using a Razor View called Login
        }

        var result = await _signInManager.PasswordSignInAsync(
            model.Email,
            model.Password,
            isPersistent: isPersistent, // Use the Remember Me value
            lockoutOnFailure: true);

        if (result.Succeeded)
        {
            // NEW STEP: Fetch User to check roles for targeted redirection
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user != null)
            {
                
                if (await _userManager.IsInRoleAsync(user, "Admin"))
                    return RedirectToAction("Dashboard", "Admin");
                else if (await _userManager.IsInRoleAsync(user, "Teacher"))
                    return RedirectToAction("Dashboard", "Teacher");
                else 
                    return RedirectToAction("Dashboard", "Student");
            }

            return LocalRedirect(returnUrl ?? "/");
        }

        ViewBag.Error = "Invalid Student ID or Password.";
        return View(model);
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

        return View(model);
    }

    [HttpPost("register/{roleName}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterModel model)
    {
        if (ModelState.IsValid)
        {
            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                if (await _roleManager.RoleExistsAsync(model.RoleName))
                {
                    await _userManager.AddToRoleAsync(user, model.RoleName);
                }
                else
                {
                    // Log an error if the role doesn't exist, but still redirect user
                    // (This should be prevented by your seeding logic)
                    // You might consider deleting the user if role assignment is mandatory.
                }

                await _signInManager.SignInAsync(user, isPersistent: false);

                if (model.RoleName == "Admin")
                    return RedirectToAction("Dashboard", "Admin");
                else if (model.RoleName == "Teacher")
                    return RedirectToAction("Dashboard", "Teacher");
                else
                    return RedirectToAction("Dashboard", "Student");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        return View(model);
    }
    #endregion
}