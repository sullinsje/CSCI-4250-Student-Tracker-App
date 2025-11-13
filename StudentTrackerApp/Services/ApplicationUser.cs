using Microsoft.AspNetCore.Identity;
using StudentTrackerApp.Models.Entities; // Needed to reference the Student class

namespace StudentTrackerApp.Services;

public class ApplicationUser : IdentityUser
{
    // NEW: Add a required property for the user's full name
    public string Name { get; set; } = string.Empty;

    // Navigation property for the one-to-one relationship with Student.
    public Student? Student { get; set; }
}