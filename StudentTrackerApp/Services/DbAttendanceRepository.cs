using StudentTrackerApp.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace StudentTrackerApp.Services;

public class DbAttendanceRepository : IAttendanceRepository
{
    private readonly ApplicationDbContext _db;

    public DbAttendanceRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<AttendanceRecord> CreateAsync(int studentId, AttendanceRecord newRecord)
    {
        // Ensure the FK is correctly set
        newRecord.StudentId = studentId;
        
        await _db.AttendanceRecords.AddAsync(newRecord);
        await _db.SaveChangesAsync();
        return newRecord;
    }

    public async Task<ICollection<AttendanceRecord>> ReadAllByStudentAsync(int studentId)
    {
        // Filter by the StudentId foreign key
        return await _db.AttendanceRecords
            .Where(r => r.StudentId == studentId)
            .OrderByDescending(r => r.Date) // Order by latest first
            .ToListAsync();
    }
    
    public async Task<AttendanceRecord?> ReadAsync(int recordId)
    {
        // Read a single record by its primary key
        return await _db.AttendanceRecords
            .FirstOrDefaultAsync(r => r.Id == recordId);
    }
}