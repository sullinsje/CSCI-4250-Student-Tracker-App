using StudentTrackerApp.Models.Entities;

namespace StudentTrackerApp.Services;

public interface IStudentRepository
{
    Task<ICollection<Student>> ReadAllAsync();
    Task<Student> CreateAsync(Student newStudent);
    Task<Student?> ReadAsync(int id);
    Task UpdateAsync(int oldId, Student updatedStudent);
    Task DeleteAsync(int id);
    
}