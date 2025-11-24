using Microsoft.AspNetCore.Identity;
namespace StudentTrackerApp.Services;

/// <summary>
/// Abstraction over ASP.NET Identity operations used by the application.
/// Provides read/write operations for <see cref="ApplicationUser"/> and role management helpers.
/// </summary>
public interface IIdentityUserRepository
{
    // READ
    /// <summary>
    /// Reads all application users.
    /// </summary>
    Task<ICollection<ApplicationUser>> ReadAllAsync();

    /// <summary>
    /// Reads a single user by id.
    /// </summary>
    Task<ApplicationUser?> ReadAsync(string id);

    // CREATE
    /// <summary>
    /// Creates a new user with a password.
    /// </summary>
    Task<IdentityResult> CreateUserWithPasswordAsync(ApplicationUser newApplicationUser, string password);
    
    // UPDATE
    /// <summary>
    /// Updates an application user.
    /// </summary>
    Task<IdentityResult> UpdateAsync(string id, ApplicationUser updatedApplicationUser);
    
    // DELETE
    /// <summary>
    /// Deletes an application user by id.
    /// </summary>
    Task<IdentityResult> DeleteAsync(string id);

    // ROLE METHODS
    /// <summary>
    /// Retrieves roles for a user.
    /// </summary>
    Task<ICollection<string>> GetRolesAsync(ApplicationUser user);

    /// <summary>
    /// Returns the names of all roles defined in the system.
    /// </summary>
    Task<ICollection<string>> ReadAllRoleNamesAsync();

    /// <summary>
    /// Replaces the user's roles with a single primary role.
    /// </summary>
    Task<IdentityResult> SetPrimaryRoleAsync(ApplicationUser user, string newRoleName);
}