using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity; // Needed for IdentityDbContext base
using StudentTrackerApp.Models.Entities;

namespace StudentTrackerApp.Services;

// The context now inherits from IdentityDbContext, which brings in the User, Role, etc., tables
// and uses your ApplicationUser (which we assume exists and inherits from IdentityUser).
public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // 1. DbSets for your application entities
    public DbSet<Student> Students { get; set; }
    public DbSet<AttendanceRecord> AttendanceRecords { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // 2. IMPORTANT: Must call base method for Identity tables to be configured first
        base.OnModelCreating(modelBuilder);

        // --- Student-to-ApplicationUser (One-to-One) Configuration ---
        modelBuilder.Entity<Student>()
            // Student has one User
            .HasOne<ApplicationUser>() 
            .WithOne(u => u.Student) // We need to add a Student navigation property to ApplicationUser
            .HasForeignKey<Student>(s => s.UserId)
            .IsRequired(); // A Student record must link to a User

        // --- AttendanceRecord-to-Student (Many-to-One) Configuration ---
        modelBuilder.Entity<AttendanceRecord>()
            // AttendanceRecord has one Student
            .HasOne(a => a.Student)
            // Student has many AttendanceRecords
            .WithMany(s => s.AttendanceRecords)
            // The FK is stored in the AttendanceRecord table (named StudentId)
            .HasForeignKey(a => a.StudentId)
            .OnDelete(DeleteBehavior.Cascade); // If the Student is deleted, attendance records are too
            
        // Fix for EF Core: Ensure Student.id is used as PK and AttendanceRecord.StudentId matches type.
        // Your current Student.cs uses int for 'id', but AttendanceRecord.cs uses string for 'StudentId'.
        // We need to ensure the foreign key matches the primary key type.
    }
}