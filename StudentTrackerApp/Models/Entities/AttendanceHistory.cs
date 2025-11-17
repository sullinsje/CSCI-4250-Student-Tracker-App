using System;

namespace StudentTrackerApp.Models.Entities;

public class AttendanceHistory
{
    public int Id { get; set; }
    public DateOnly ClockInTime { get; set; }
    public double ClockInLatitude { get; set; }
    public double ClockInLongitude { get; set; }

    //Foreign Keys
    public int StudentId { get; set; } 
    public Student Student { get; set; } = null!; 
}