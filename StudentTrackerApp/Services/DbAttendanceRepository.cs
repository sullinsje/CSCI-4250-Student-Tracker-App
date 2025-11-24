using StudentTrackerApp.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace StudentTrackerApp.Services;

/// <summary>
/// EF Core backed implementation of <see cref="IAttendanceRepository"/>.
/// Provides methods to create and query <see cref="Models.Entities.AttendanceRecord"/> entities.
/// </summary>
public class DbAttendanceRepository : IAttendanceRepository
{
    private readonly ApplicationDbContext _db;

    public DbAttendanceRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    /// <summary>
    /// Creates a new attendance record for the provided student id.
    /// </summary>
    /// <param name="studentId">Student id FK for the record.</param>
    /// <param name="newRecord">The attendance record to create.</param>
    /// <returns>The stored attendance record.</returns>
    public async Task<AttendanceRecord> CreateAsync(int studentId, AttendanceRecord newRecord)
    {
        // Ensure the FK is correctly set
        newRecord.StudentId = studentId;
        
        await _db.AttendanceRecords.AddAsync(newRecord);
        await _db.SaveChangesAsync();
        return newRecord;
    }

    /// <summary>
    /// Reads all attendance records for a specific student ordered by date (newest first).
    /// </summary>
    /// <param name="studentId">Student id to filter by.</param>
    /// <returns>Collection of attendance records.</returns>
    public async Task<ICollection<AttendanceRecord>> ReadAllByStudentAsync(int studentId)
    {
        // Filter by the StudentId foreign key
        return await _db.AttendanceRecords
            .Where(r => r.StudentId == studentId)
            .OrderByDescending(r => r.Date) // Order by latest first
            .ToListAsync();
    }
    
    /// <summary>
    /// Reads a single attendance record by its primary key.
    /// </summary>
    /// <param name="recordId">Attendance record id.</param>
    /// <returns>The attendance record or null if not found.</returns>
    public async Task<AttendanceRecord?> ReadAsync(int recordId)
    {
        // Read a single record by its primary key
        return await _db.AttendanceRecords
            .FirstOrDefaultAsync(r => r.Id == recordId);
    }
}