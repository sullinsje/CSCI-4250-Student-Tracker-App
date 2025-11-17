using System.Text.Json.Serialization;

namespace StudentTrackerApp.Models.Entities;

public class AttendanceRecord
{
    public int Id { get; set; }
    public int StudentId { get; set; } 
    public bool ClockType {get; set; }
    public DateOnly Date { get; set; }
    public double ClockInLatitude { get; set; }
    public double ClockInLongitude { get; set; }
    [JsonIgnore]
    public Student Student { get; set; } = null!; 
}