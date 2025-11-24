using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudentTrackerApp.Services;

/// <summary>
/// Implementation of <see cref="IIdentityUserRepository"/> that uses ASP.NET
/// Identity's <see cref="UserManager{TUser}"/> and <see cref="RoleManager{TRole}"/>
/// together with <see cref="ApplicationDbContext"/> to manage application users and roles.
/// </summary>
public class DbIdentityUserRepository : IIdentityUserRepository
{
    // UserManager handles all security and persistence logic, RoleManager helps list roles
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager; 
    private readonly ApplicationDbContext _db;

    /// <summary>
    /// Creates a new <see cref="DbIdentityUserRepository"/>.
    /// </summary>
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

    /// <summary>
    /// Reads all application users.
    /// </summary>
    public async Task<ICollection<ApplicationUser>> ReadAllAsync()
    {
        // db since we already have it
        return await _db.Users.ToListAsync();
    }

    // Use string for ID since Identity uses it
    /// <summary>
    /// Reads a single application user by identity id.
    /// </summary>
    /// <param name="id">Identity user id.</param>
    public async Task<ApplicationUser?> ReadAsync(string id)
    {
        // userManager is better here for getting by the user's Id
        return await _userManager.FindByIdAsync(id);
    }
    
    // ROLE OPERATIONS
    
    /// <summary>
    /// Retrieves all roles currently assigned to the specified user.
    /// </summary>
    /// <summary>
    /// Retrieves roles currently assigned to the specified user.
    /// </summary>
    /// <param name="user">User to inspect.</param>
    public async Task<ICollection<string>> GetRolesAsync(ApplicationUser user)
    {
        // Uses UserManager to fetch roles from the AspNetUserRoles table.
        return await _userManager.GetRolesAsync(user);
    }
    
    /// <summary>
    /// Retrieves a list of all defined role names in the system.
    /// </summary>
    /// <summary>
    /// Returns all available role names defined in the system.
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
    /// <summary>
    /// Replaces all roles on the user with the provided primary role.
    /// </summary>
    /// <param name="user">User to update.</param>
    /// <param name="newRoleName">Role name to set as the primary role.</param>
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

    /// <summary>
    /// Creates a new identity user with the provided password.
    /// </summary>
    /// <param name="newApplicationUser">User to create.</param>
    /// <param name="password">Plain text password to set (UserManager handles hashing).</param>
    public async Task<IdentityResult> CreateUserWithPasswordAsync(ApplicationUser newApplicationUser, string password)
    {
        // UserManager handles hashing the password, validation, and saving user
        return await _userManager.CreateAsync(newApplicationUser, password);
    }

    // again use UserManager for ease of use and data integrity
    /// <summary>
    /// Updates an application user by id.
    /// </summary>
    /// <param name="id">User id.</param>
    /// <param name="updatedApplicationUser">Updated user values.</param>
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
    /// <summary>
    /// Deletes an application user by id.
    /// </summary>
    /// <param name="id">User id to delete.</param>
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