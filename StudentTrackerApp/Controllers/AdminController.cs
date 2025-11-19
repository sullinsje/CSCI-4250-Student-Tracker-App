using Microsoft.AspNetCore.Mvc;
using StudentTrackerApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity; 

namespace StudentTrackerApp.Controllers;

// --- DTOs for MVC form binding ---
// NOTE: These DTOs are defined here for simplicity but should ideally be in a shared Models/DTOs folder.
public class UserCreationDto
{
    public required string Email { get; set; }
    public required string Name { get; set; }
    public required string Password { get; set; }
    public required string RoleName { get; set; } 
}

public class UserUpdateDto
{
    public required string Name { get; set; }
    
    public required string RoleName { get; set; } 
}


[Authorize(Roles = "Admin")] // Ensures only users with the "Admin" role can access any action
public class AdminController : Controller
{
    private readonly IIdentityUserRepository _userRepo;

    public AdminController(IIdentityUserRepository userRepo)
    {
        _userRepo = userRepo;
    }

    // READ ALL (Index)
    // GET: /Admin/Index
    public async Task<IActionResult> Index()
    {
        ViewData["Title"] = "Manage Users Dashboard";
        var users = await _userRepo.ReadAllAsync();
        // Pass the list of ApplicationUser entities to the view 
        return View(users);
    }

    // READ ONE (User Details)
    // GET: /Admin/Details/{id}
    [HttpGet]
    public async Task<IActionResult> Details(string id)
    {
        var user = await _userRepo.ReadAsync(id);
        
        if (user == null)
        {
            return NotFound();
        }

        // pass user's name into view
        ViewData["Title"] = $"Details for {user.Name}";
        return View(user);
    }
    
    // CREATE OPERATIONS
    // GET: /Admin/Create
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        ViewData["Title"] = "Create New User";
        // Pass roles to the view for initial assignment
        ViewData["AllRoles"] = await _userRepo.ReadAllRoleNamesAsync(); 
        // Pass an empty DTO to ensure the view can render the form fields correctly
        return View(new UserCreationDto { Email = string.Empty, Name = string.Empty, Password = string.Empty, RoleName = string.Empty });
    }

    // POST: /Admin/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(UserCreationDto dto)
    {
        // Re-fetch roles in case of error
        ViewData["AllRoles"] = await _userRepo.ReadAllRoleNamesAsync(); 
        
        // Ensure the RoleName is not empty if roles were loaded
        if (string.IsNullOrEmpty(dto.RoleName) && (ViewData["AllRoles"] as ICollection<string>)?.Any() == true)
        {
             ModelState.AddModelError(nameof(dto.RoleName), "A user role must be selected.");
        }

        if (ModelState.IsValid)
        {
            var newUser = new ApplicationUser 
            {
                UserName = dto.Email,
                Email = dto.Email,
                Name = dto.Name,
                EmailConfirmed = true 
            };

            // Create the user with password
            IdentityResult createResult = await _userRepo.CreateUserWithPasswordAsync(newUser, dto.Password);

            if (createResult.Succeeded)
            {
                // Assign the role
                IdentityResult roleResult = await _userRepo.SetPrimaryRoleAsync(newUser, dto.RoleName);

                if (roleResult.Succeeded)
                {
                    TempData["SuccessMessage"] = $"User '{dto.Name}' created successfully with role '{dto.RoleName}'.";
                    return RedirectToAction(nameof(Index)); 
                }
                
                // If role assignment fails, report the error and attempt to clean up the user
                foreach (var error in roleResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, $"Role Assignment Failed: {error.Description}");
                }
            }
            else
            {
                 // If user creation fails, capture those errors
                foreach (var error in createResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
        }
        
        ViewData["Title"] = "Create New User";
        return View(dto);
    }

    // UPDATE OPERATIONS

    // GET: /Admin/Edit/{id}
    [HttpGet]
    public async Task<IActionResult> Edit(string id)
    {
        var user = await _userRepo.ReadAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        // Get all available roles
        ViewData["AllRoles"] = await _userRepo.ReadAllRoleNamesAsync();
        // Get the user's current roles
        var currentRoles = await _userRepo.GetRolesAsync(user);
        
        // Map ApplicationUser (user) to DTO for the form
        ViewData["UserId"] = user.Id; 
        var dto = new UserUpdateDto 
        { 
            Name = user.Name,
            RoleName = currentRoles.FirstOrDefault() ?? string.Empty 
        };
        
        ViewData["Title"] = $"Edit User: {user.Name}";
        return View(dto);
    }

    // POST: /Admin/Edit/{id}
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string id, UserUpdateDto dto)
    {
        // Ensure roles are re-fetched in case of validation failure
        ViewData["AllRoles"] = await _userRepo.ReadAllRoleNamesAsync();
        ViewData["UserId"] = id; 
        
        if (ModelState.IsValid)
        {
            var userToUpdate = await _userRepo.ReadAsync(id);

            if (userToUpdate == null)
            {
                return NotFound();
            }

            // Update basic user details (Name)
            userToUpdate.Name = dto.Name;
            
            IdentityResult updateResult = await _userRepo.UpdateAsync(id, userToUpdate);

            // Update Role if provided
            IdentityResult roleResult = IdentityResult.Success; // Start as success
            if (!string.IsNullOrEmpty(dto.RoleName))
            {
                roleResult = await _userRepo.SetPrimaryRoleAsync(userToUpdate, dto.RoleName);
            }

            // Check combined results
            if (updateResult.Succeeded && roleResult.Succeeded)
            {
                TempData["SuccessMessage"] = $"User '{userToUpdate.Name}' updated successfully (Role: {dto.RoleName}).";
                return RedirectToAction(nameof(Index)); 
            }
            
            // Collect all errors (user update errors + role update errors)
            var allErrors = updateResult.Errors.Concat(roleResult.Errors);
            foreach (var error in allErrors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        
        ViewData["Title"] = "Edit User";
        return View(dto);
    }
    
    // DELETE OPERATIONS

    // GET: /Admin/Delete/{id} (Confirmation View)
    [HttpGet]
    public async Task<IActionResult> Delete(string id)
    {
        var user = await _userRepo.ReadAsync(id);
        
        if (user == null)
        {
            // If the user doesn't exist, treat it as a successful check and redirect to the list
            return RedirectToAction(nameof(Index)); // Redirect to the main Index/List page
        }

        ViewData["Title"] = "Confirm Deletion";
        return View(user); 
    }

    // POST: /Admin/DeleteConfirmed/{id} (Actual Deletion)
    [HttpPost, ActionName("Delete")] 
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        var result = await _userRepo.DeleteAsync(id);

        if (result.Succeeded)
        {
            TempData["SuccessMessage"] = "User deleted successfully.";
            return RedirectToAction(nameof(Index)); // Redirect to the main Index/List page
        }

        // Handle errors, if deletion failed 
        TempData["ErrorMessage"] = "Failed to delete user. Please try again.";
        // Redirect back to the list view after attempting deletion
        return RedirectToAction(nameof(Index)); // Redirect to the main Index/List page
    }
}