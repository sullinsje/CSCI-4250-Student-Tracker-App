using Microsoft.EntityFrameworkCore;
using StudentTrackerApp.Models.Entities;

namespace StudentTrackerApp.Services;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    public DbSet<Student> Students => Set<Student>();
    public DbSet<AttendanceHistory> AttendanceHistory => Set<AttendanceHistory>();
}