using Microsoft.AspNetCore.Identity;
namespace StudentTrackerApp.Services;
public interface IIdentityUserRepository
{
    // READ
    Task<ICollection<ApplicationUser>> ReadAllAsync();
    Task<ApplicationUser?> ReadAsync(string id);

    // CREATE
    Task<IdentityResult> CreateUserWithPasswordAsync(ApplicationUser newApplicationUser, string password);
    
    // UPDATE
    Task<IdentityResult> UpdateAsync(string id, ApplicationUser updatedApplicationUser);
    
    // DELETE
    Task<IdentityResult> DeleteAsync(string id);

    // ROLE METHODS
    Task<ICollection<string>> GetRolesAsync(ApplicationUser user);
    Task<ICollection<string>> ReadAllRoleNamesAsync();
    Task<IdentityResult> SetPrimaryRoleAsync(ApplicationUser user, string newRoleName);
}