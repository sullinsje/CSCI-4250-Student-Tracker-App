namespace StudentTrackerApp.Models.Entities;

public class AttendanceRecord
{
    public int Id { get; set; }
    public int StudentId { get; set; } 
    public DateOnly Date { get; set; }
    public double ClockInLatitude { get; set; }
    public double ClockInLongitude { get; set; }
    public Student Student { get; set; } = null!; 
}