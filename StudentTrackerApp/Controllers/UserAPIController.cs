using Microsoft.AspNetCore.Mvc;
using StudentTrackerApp.Services;
using Microsoft.AspNetCore.Authorization;

namespace StudentTracker.Controllers;

// --- DTOs for API Input ---
// These classes capture data from the client, preventing over-posting 
// and ensuring the password is only handled during creation.
public class UserCreationDto
{
    public required string Email { get; set; }
    public required string Name { get; set; }
    public required string Password { get; set; }
}

public class UserUpdateDto
{
    public required string Name { get; set; }
    // We only need the name for this dto since that is all we actually update
}


[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")] 
/// <summary>
/// API controller for managing application identity users. Restricted to Admin role.
/// Provides CRUD endpoints for users used by administrative clients.
/// </summary>
public class UserAPIController : ControllerBase
{
    private readonly IIdentityUserRepository _userRepo;

    public UserAPIController(IIdentityUserRepository userRepo)
    {
        _userRepo = userRepo;
    }

    // READ ALL
    /// <summary>
    /// Returns all identity users.
    /// </summary>
    [HttpGet("all")]
    public async Task<IActionResult> Get()
    {
        var users = await _userRepo.ReadAllAsync();
        return Ok(users);
    }

    // READ ONE
    /// <summary>
    /// Returns a single user by id.
    /// </summary>
    /// <param name="id">User id to fetch.</param>
    [HttpGet("one/{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var user = await _userRepo.ReadAsync(id);
        
        if (user == null)
        {
            return NotFound($"User with ID {id} not found.");
        }
        
        return Ok(user);
    }

    // CREATE 
    /// <summary>
    /// Creates a new identity user using provided DTO.
    /// </summary>
    /// <param name="dto">User creation DTO.</param>
    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] UserCreationDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // make actual user from the dto
        var newUser = new ApplicationUser 
        {
            UserName = dto.Email, 
            Email = dto.Email,
            Name = dto.Name,
            EmailConfirmed = true 
        };

        var result = await _userRepo.CreateUserWithPasswordAsync(newUser, dto.Password);

        if (result.Succeeded)
        {
            //return new user
            return CreatedAtAction(nameof(Get), new { id = newUser.Id }, newUser);
        }

        return BadRequest(new { errors = result.Errors.Select(e => e.Description) });
    }

    // EDIT
    /// <summary>
    /// Updates a user's editable fields.
    /// </summary>
    /// <param name="id">User id to update.</param>
    /// <param name="dto">Update DTO.</param>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] UserUpdateDto dto)
    {
        var userToUpdate = await _userRepo.ReadAsync(id);

        if (userToUpdate == null)
        {
            return NotFound($"User with ID {id} not found.");
        }
        
        // we only update the name when managing users; other stuff is handled with User/RoleManager
        userToUpdate.Name = dto.Name;

        var result = await _userRepo.UpdateAsync(id, userToUpdate);

        if (result.Succeeded)
        {
            return Ok(userToUpdate); 
        }

        return BadRequest(new { errors = result.Errors.Select(e => e.Description) });
    }

    // DELETE
    /// <summary>
    /// Deletes a user by id.
    /// </summary>
    /// <param name="id">User id to delete.</param>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var result = await _userRepo.DeleteAsync(id);

        if (result.Succeeded)
        {
            // Return 204 No Content (standard for successful deletion)
            return NoContent(); 
        }
        
        if (result.Errors.Any(e => e.Description.Contains("not found")))
        {
            return NotFound($"User with ID {id} not found.");
        }

        return BadRequest(new { errors = result.Errors.Select(e => e.Description) });
    }
}