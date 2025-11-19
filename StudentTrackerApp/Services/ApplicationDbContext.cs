using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using StudentTrackerApp.Models.Entities;

namespace StudentTrackerApp.Services;

// Context inherits from IdentityDbContext, which brings in the User/Role tables
// and uses ApplicationUser
public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Student> Students { get; set; }
    public DbSet<AttendanceRecord> AttendanceRecords { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Student-to-ApplicationUser (One-to-One)
        modelBuilder.Entity<Student>()
            .HasOne<ApplicationUser>()
            .WithOne(u => u.Student)
            .HasForeignKey<Student>(s => s.UserId)
            .IsRequired();

        // AttendanceRecord-to-Student (Many-to-One) 
        // AttendanceRecord has one Student
        // Student has many AttendanceRecords
        modelBuilder.Entity<AttendanceRecord>()
            .HasOne(a => a.Student)
            .WithMany(s => s.AttendanceRecords)
            .HasForeignKey(a => a.StudentId)
            .OnDelete(DeleteBehavior.Cascade); // If Student is deleted, attendance records are too

    }
}