using StudentTrackerApp.Models.Entities;

namespace StudentTrackerApp.Services;

public interface IAttendanceRepository
{
    // Records a new attendance entry for a specific student.
    Task<AttendanceRecord> CreateAsync(int studentId, AttendanceRecord newRecord);

    // Reads all attendance records for a specific student.
    Task<ICollection<AttendanceRecord>> ReadAllByStudentAsync(int studentId);

    // Optional: Reads a single specific attendance record.
    Task<AttendanceRecord?> ReadAsync(int recordId);
}