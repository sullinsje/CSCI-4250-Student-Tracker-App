using StudentTrackerApp.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace StudentTrackerApp.Services;

/// <summary>
/// EF Core implementation of <see cref="IStudentRepository"/> that uses
/// <see cref="ApplicationDbContext"/> to perform CRUD operations against students
/// and their attendance records.
/// </summary>
public class DbStudentRepository : IStudentRepository
{
    private readonly ApplicationDbContext _db;

    public DbStudentRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    /// <summary>
    /// Reads all students including their attendance collections.
    /// </summary>
    /// <returns>Collection of students.</returns>
    public async Task<ICollection<Student>> ReadAllAsync()
    {
        return await _db.Students
            .Include(s => s.AttendanceRecords)
            .ToListAsync();
    }

    /// <summary>
    /// Adds a new student to the database.
    /// </summary>
    /// <param name="newStudent">Student instance to add.</param>
    /// <returns>The created student with generated Id.</returns>
    public async Task<Student> CreateAsync(Student newStudent)
    {
        await _db.Students.AddAsync(newStudent); //add to db
        await _db.SaveChangesAsync(); //save changes
        return newStudent;
    }

    /// <summary>
    /// Reads a single student by id and eagerly loads the attendance collection.
    /// </summary>
    /// <param name="id">Student id.</param>
    /// <returns>The student or null if not found.</returns>
    public async Task<Student?> ReadAsync(int id)
    {
        var student = await _db.Students.FindAsync(id); // find instead of ToListAsync()
        if (student != null)
        {
            _db.Entry(student)
                .Collection(s => s.AttendanceRecords)
                .Load(); // Note the collection of student records and load it into student data
        }

        return student;
    }

    /// <summary>
    /// Updates a student's basic information. Currently only updates the <see cref="Student.Name"/>.
    /// </summary>
    /// <param name="oldId">Id of the student to update.</param>
    /// <param name="updatedStudent">Student instance containing updated values.</param>
    public async Task UpdateAsync(int oldId, Student updatedStudent)
    {
        var studentToUpdate = await ReadAsync(oldId);

        if (studentToUpdate != null)
        {
            studentToUpdate.Name = updatedStudent.Name; // the scope of project requires only name updates

            _db.Students.Update(studentToUpdate);
            await _db.SaveChangesAsync();
        }

    }

    /// <summary>
    /// Deletes a student and cascades delete to attendance records.
    /// </summary>
    /// <param name="id">Student id to delete.</param>
    public async Task DeleteAsync(int id)
    {
        var studentToDelete = await ReadAsync(id);
        if (studentToDelete != null)
        {
            _db.Students.Remove(studentToDelete);
            await _db.SaveChangesAsync();
        }
    }

    /// <summary>
    /// Attaches an attendance record to an existing student and saves changes.
    /// </summary>
    /// <param name="studentId">Id of the student to attach the record to.</param>
    /// <param name="attendanceRecord">Attendance record to add.</param>
    /// <returns>The created attendance record.</returns>
    public async Task<AttendanceRecord> CreateAttendanceRecordAsync(int studentId, AttendanceRecord attendanceRecord)
    {
        var student = await ReadAsync(studentId);

        if (student != null)
        {
            student.AttendanceRecords.Add(attendanceRecord);
            await _db.SaveChangesAsync();
        }

        return attendanceRecord;
    }

}