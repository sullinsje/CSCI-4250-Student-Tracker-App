using StudentTrackerApp.Models.Entities;

namespace StudentTrackerApp.Services;

/// <summary>
/// Repository abstraction for attendance related operations.
/// </summary>
public interface IAttendanceRepository
{
    /// <summary>
    /// Records a new attendance entry for a specific student.
    /// </summary>
    /// <param name="studentId">Student id to attach the record to.</param>
    /// <param name="newRecord">New attendance record instance.</param>
    Task<AttendanceRecord> CreateAsync(int studentId, AttendanceRecord newRecord);

    /// <summary>
    /// Reads all attendance records for a specific student.
    /// </summary>
    /// <param name="studentId">Student id to filter by.</param>
    Task<ICollection<AttendanceRecord>> ReadAllByStudentAsync(int studentId);

    /// <summary>
    /// Reads a single specific attendance record.
    /// </summary>
    /// <param name="recordId">Attendance record id.</param>
    Task<AttendanceRecord?> ReadAsync(int recordId);
}