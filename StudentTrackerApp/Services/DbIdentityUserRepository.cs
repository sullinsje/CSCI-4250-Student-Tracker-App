using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudentTrackerApp.Services;

public class DbIdentityUserRepository : IIdentityUserRepository
{
    // UserManager handles all security and persistence logic, RoleManager helps list roles
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager; 
    private readonly ApplicationDbContext _db;

    public DbIdentityUserRepository(
        UserManager<ApplicationUser> userManager, 
        RoleManager<IdentityRole> roleManager, 
        ApplicationDbContext db)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _db = db;
    }

    // READ OPERATIONS 

    public async Task<ICollection<ApplicationUser>> ReadAllAsync()
    {
        // db since we already have it
        return await _db.Users.ToListAsync();
    }

    // Use string for ID since Identity uses it
    public async Task<ApplicationUser?> ReadAsync(string id)
    {
        // userManager is better here for getting by the user's Id
        return await _userManager.FindByIdAsync(id);
    }
    
    // ROLE OPERATIONS
    
    /// <summary>
    /// Retrieves all roles currently assigned to the specified user.
    /// </summary>
    public async Task<ICollection<string>> GetRolesAsync(ApplicationUser user)
    {
        // Uses UserManager to fetch roles from the AspNetUserRoles table.
        return await _userManager.GetRolesAsync(user);
    }
    
    /// <summary>
    /// Retrieves a list of all defined role names in the system.
    /// </summary>
    public async Task<ICollection<string>> ReadAllRoleNamesAsync()
    {
        // Uses RoleManager to retrieve all role names from the AspNetRoles table.
        return await _roleManager.Roles
            .Select(r => r.Name!) 
            .ToListAsync();
    }

    /// <summary>
    /// Removes all existing roles from the user and assigns a single new role.
    /// </summary>
    public async Task<IdentityResult> SetPrimaryRoleAsync(ApplicationUser user, string newRoleName)
    {
        // Get the current roles for the user
        var currentRoles = await _userManager.GetRolesAsync(user);
        
        // Remove all current roles
        IdentityResult removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
        if (!removeResult.Succeeded)
        {
            return removeResult;
        }

        // Add the new role
        return await _userManager.AddToRoleAsync(user, newRoleName);
    }

    //  WRITE OPERATIONS (use UserManager) 

    public async Task<IdentityResult> CreateUserWithPasswordAsync(ApplicationUser newApplicationUser, string password)
    {
        // UserManager handles hashing the password, validation, and saving user
        return await _userManager.CreateAsync(newApplicationUser, password);
    }

    // again use UserManager for ease of use and data integrity
    public async Task<IdentityResult> UpdateAsync(string id, ApplicationUser updatedApplicationUser)
    {
        var userToUpdate = await ReadAsync(id);
        
        if (userToUpdate == null)
        {
            // Return a failed result if user not found.
            return IdentityResult.Failed(new IdentityError { Description = $"User with ID {id} not found." });
        }
        
        // Transfer only name
        userToUpdate.Name = updatedApplicationUser.Name; 
        
        return await _userManager.UpdateAsync(userToUpdate);
    }
    
    // Using UserManager handles cascading deletes
    public async Task<IdentityResult> DeleteAsync(string id)
    {
        var userToDelete = await ReadAsync(id);
        if (userToDelete != null)
        {
            return await _userManager.DeleteAsync(userToDelete);
        }
        
        return IdentityResult.Failed(new IdentityError { Description = $"User with ID {id} not found for deletion." });
    }

}