using System.Text.Json.Serialization;

namespace StudentTrackerApp.Models.Entities;

/// <summary>
/// Represents an attendance record for a student. Date is stored as a <see cref="DateOnly"/>
/// and geolocation is captured as latitude/longitude values.
/// </summary>
public class AttendanceRecord
{
    /// <summary>
    /// Primary key for the attendance record.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Foreign key reference to the owning <see cref="Student"/>.
    /// </summary>
    public int StudentId { get; set; } 

    /// <summary>
    /// True if this record represents a clock-in event; false for clock-out.
    /// </summary>
    public bool ClockType {get; set; }

    /// <summary>
    /// The date of the record (date-only, no time component).
    /// </summary>
    public DateOnly Date { get; set; }

    /// <summary>
    /// Latitude captured at clock-in.
    /// </summary>
    public double ClockInLatitude { get; set; }

    /// <summary>
    /// Longitude captured at clock-in.
    /// </summary>
    public double ClockInLongitude { get; set; }

    [JsonIgnore]
    public Student Student { get; set; } = null!; 
}