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
        var student = await _db.Students.FindAsync(id);
        if (student != null)
        {
            _db.Entry(student)
                .Collection(s => s.AttendanceRecords)
                .Load();
        }

        return student;
    }


    public async Task UpdateAsync(int oldId, Student updatedStudent)
    {
        // Retrieve the existing entity using the correct Id property
        var studentToUpdate = await ReadAsync(oldId);
        
        if (studentToUpdate != null)
        {
            // FIX: Accessing the Name property using PascalCase
            studentToUpdate.Name = updatedStudent.Name; 
            
            // Note: Since UserId is a Foreign Key to the Identity system, 
            // it typically should not be updated here.
            
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
        var student = await _db.Students
            .Include(s => s.AttendanceRecords)
            .FirstOrDefaultAsync(s => s.Id == studentId);

        if (student == null)
        {
            throw new System.Exception($"Book with ID {studentId} not found.");
        }

        student.AttendanceRecords.Add(attendanceRecord);

        await _db.SaveChangesAsync();

        return attendanceRecord;
    }
    
}