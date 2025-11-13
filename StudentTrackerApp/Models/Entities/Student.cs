namespace StudentTrackerApp.Models.Entities;

public class Student
{
    // Changed property name to standard PascalCase 'Id'
    public int Id { get; set; }
    
    // Changed property name to standard PascalCase 'Name'
    public string Name { get; set; } = String.Empty;

    // Changed property name to standard PascalCase 'UserId'
    // This is the FK to the Identity User (string type)
    public string UserId { get; set; } = String.Empty;

    // Navigation property for the User entity (assuming it is ApplicationUser)
    // You should add this property to ApplicationUser as well: public Student? Student { get; set; }
    // public ApplicationUser User { get; set; } // Add this if you want to navigate from Student to User

    public ICollection<AttendanceRecord> AttendanceRecords { get; set; } = new List<AttendanceRecord>();
}