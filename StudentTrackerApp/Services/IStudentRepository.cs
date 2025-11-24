using StudentTrackerApp.Models.Entities;

namespace StudentTrackerApp.Services;

/// <summary>
/// Repository abstraction for student CRUD operations and attaching attendance records.
/// </summary>
public interface IStudentRepository
{
    /// <summary>
    /// Reads all students including attendance data.
    /// </summary>
    Task<ICollection<Student>> ReadAllAsync();

    /// <summary>
    /// Creates a new student.
    /// </summary>
    /// <param name="newStudent">Student instance to create.</param>
    Task<Student> CreateAsync(Student newStudent);

    /// <summary>
    /// Reads a student by id.
    /// </summary>
    Task<Student?> ReadAsync(int id);

    /// <summary>
    /// Updates an existing student's data.
    /// </summary>
    Task UpdateAsync(int oldId, Student updatedStudent);

    /// <summary>
    /// Deletes an existing student.
    /// </summary>
    Task DeleteAsync(int id);

    /// <summary>
    /// Adds an attendance record for a student.
    /// </summary>
    Task<AttendanceRecord> CreateAttendanceRecordAsync(int studentId, AttendanceRecord attendanceRecord);
    
}