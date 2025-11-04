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
        return await _db.Students.ToListAsync();
    }

    public async Task<Student> CreateAsync(Student newStudent)
    {
        await _db.Students.AddAsync(newStudent); //add to db
        await _db.SaveChangesAsync(); //save changes
        return newStudent;
    }

    public async Task<Student?> ReadAsync(int id)
    {
        return await _db.Students.FindAsync(id);
    }


    public async Task UpdateAsync(int oldId, Student updatedStudent)
    {
        var studentToUpdate = await ReadAsync(oldId);
        if (studentToUpdate != null)
        {
            studentToUpdate.name = updatedStudent.name;
            studentToUpdate.latitude = updatedStudent.latitude;
            studentToUpdate.longitude = updatedStudent.longitude;
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
    
}