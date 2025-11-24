namespace StudentTrackerApp.Models.Entities;

/// <summary>
/// Represents a student within the application. Each student is linked
/// one-to-one with an <see cref="Services.ApplicationUser"/> via <see cref="UserId"/>.
/// </summary>
public class Student
{
    /// <summary>
    /// Primary key for the student.
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// The student's display name.
    /// </summary>
    public string Name { get; set; } = String.Empty;

    /// <summary>
    /// Identity user id that this student maps to (foreign key to AspNetUsers.Id).
    /// </summary>
    public string UserId { get; set; } = String.Empty;

    /// <summary>
    /// Collection of attendance records for the student.
    /// </summary>
    public ICollection<AttendanceRecord> AttendanceRecords { get; set; } = new List<AttendanceRecord>();
}