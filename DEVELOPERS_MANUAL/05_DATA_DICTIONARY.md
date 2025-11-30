# Data Dictionary

**Database schema, entities, and relationships reference.**

---

## Database Overview

**File Location:** `StudentTrackerApp/Data/Data.db`  
**Type:** SQLite  
**Managed By:** Entity Framework Core

---

## Core Tables

### AspNetUsers
User authentication (managed by ASP.NET Identity).

| Column | Type | Purpose |
|--------|------|---------|
| **Id** | TEXT (PK) | User identifier (GUID) |
| **UserName** | TEXT | Login name (unique) |
| **Email** | TEXT | Email address (unique) |
| **PasswordHash** | TEXT | Hashed password |
| **EmailConfirmed** | BOOLEAN | Email verified |

### AspNetRoles
Predefined roles: Admin, Teacher, Student.

| Column | Type |
|--------|------|
| **Id** | TEXT (PK) |
| **Name** | TEXT |

### AspNetUserRoles
Maps users to roles (many-to-many).

| Column | Type |
|--------|------|
| **UserId** | TEXT (FK) |
| **RoleId** | TEXT (FK) |

### Students
Student-specific data.

| Column | Type | Nullable | Purpose |
|--------|------|----------|---------|
| **Id** | INTEGER (PK) | No | Auto-increment |
| **Name** | TEXT | No | Display name |
| **UserId** | TEXT (FK) | No | Link to AspNetUsers (unique) |

### AttendanceRecords
Location tracking for clock in/out events.

| Column | Type | Purpose |
|--------|------|---------|
| **Id** | INTEGER (PK) | Auto-increment |
| **StudentId** | INTEGER (FK) | Links to Students |
| **ClockType** | BIT | true = clock in, false = clock out |
| **Date** | DATE | Date only (no time) |
| **ClockInLatitude** | DOUBLE | GPS latitude [-90, 90] |
| **ClockInLongitude** | DOUBLE | GPS longitude [-180, 180] |

---

## Entity Relationships

**One-to-One:** ApplicationUser ↔ Student
```
User with UserId = student.UserId has one Student record
```

**One-to-Many:** Student → AttendanceRecords
```
One student can have multiple attendance records
Deletion of student cascades to records
```

---

## C# Models

```csharp
public class ApplicationUser : IdentityUser
{
    public Student? Student { get; set; }
}

public class Student
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public ICollection<AttendanceRecord> AttendanceRecords { get; set; } = [];
}

public class AttendanceRecord
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public bool ClockType { get; set; }
    public DateOnly Date { get; set; }
    public double ClockInLatitude { get; set; }
    public double ClockInLongitude { get; set; }
    public Student Student { get; set; } = null!;
}
```

---

## Common Queries

```csharp
// Get student with all records
var student = await _context.Students
    .Include(s => s.AttendanceRecords)
    .FirstOrDefaultAsync(s => s.Id == studentId);

// Get attendance for date range
var records = await _context.AttendanceRecords
    .Where(r => r.StudentId == studentId && r.Date >= startDate && r.Date <= endDate)
    .OrderByDescending(r => r.Date)
    .ToListAsync();

// Get user with student data
var user = await _context.Users
    .Include(u => u.Student)
    .FirstOrDefaultAsync(u => u.Id == userId);
```

---

## Data Validation

| Field | Rule | Example |
|-------|------|---------|
| Student.Name | Required, max 100 chars | "John Smith" |
| Student.UserId | Required, unique | Valid GUID |
| AttendanceRecord.Date | Required, DateOnly | 2025-11-30 |
| Latitude | Required, [-90, 90] | 36.304516 |
| Longitude | Required, [-180, 180] | -82.412345 |

---

## Related Documents

- [05_DATA_DICTIONARY.md](05_DATA_DICTIONARY.md) - Database schema
- [03_CODING_STANDARDS.md](03_CODING_STANDARDS.md) - Data validation patterns
- [09_BUILD_DEPLOYMENT.md](09_BUILD_DEPLOYMENT.md) - Migrations

---

**Last Updated:** November 2025  
**Version:** 1.0.0
