using StudentTrackerApp.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace StudentTrackerApp.Services;

public class DbStudentRepository : IStudentRepository
{
    private readonly ApplicationDbContext _db;

    public DbStudentRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<ICollection<Student>> ReadAllAsync()
    {
        return await _db.Students
            .Include(s => s.AttendanceRecords)
            .ToListAsync();
    }

    public async Task<Student> CreateAsync(Student newStudent)
    {
        await _db.Students.AddAsync(newStudent); //add to db
        await _db.SaveChangesAsync(); //save changes
        return newStudent;
    }

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

    public async Task DeleteAsync(int id)
    {
        var studentToDelete = await ReadAsync(id);
        if (studentToDelete != null)
        {
            _db.Students.Remove(studentToDelete);
            await _db.SaveChangesAsync();
        }
    }

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