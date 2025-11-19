using Microsoft.AspNetCore.Identity;
using StudentTrackerApp.Models.Entities; 

namespace StudentTrackerApp.Services;

/// <summary>
/// allows us to store the Name of users as well as complete the 1 to 1 
/// relationship between User and Student
/// </summary>
public class ApplicationUser : IdentityUser
{
    public string Name { get; set; } = string.Empty;

    public Student? Student { get; set; }
}