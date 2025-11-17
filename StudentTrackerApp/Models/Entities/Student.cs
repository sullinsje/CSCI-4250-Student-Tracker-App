namespace StudentTrackerApp.Models.Entities;

public class Student
{
    public int Id { get; set; }
    
    public string Name { get; set; } = String.Empty;
    public string UserId { get; set; } = String.Empty;

    public ICollection<AttendanceRecord> AttendanceRecords { get; set; } = new List<AttendanceRecord>();
}