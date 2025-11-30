# Coding Standards

**C# code style guide for consistency across the project.**

---

## Naming Conventions

| Element | Convention | Example |
|---------|-----------|---------|
| Namespace | PascalCase | `StudentTrackerApp.Services` |
| Class/Interface | PascalCase, `I` prefix for interfaces | `StudentRepository`, `IStudentRepository` |
| Method | PascalCase | `GetStudentById`, `AddAttendanceRecordAsync` |
| Property | PascalCase | `Name`, `UserId`, `AttendanceRecords` |
| Private Field | camelCase, `_` prefix | `_studentRepository`, `_context` |
| Local Variable | camelCase | `studentId`, `recordCount` |
| Constant | UPPER_SNAKE_CASE or PascalCase | `MAX_RECORDS` or `MaxRecords` |
| Boolean Property | `Is`, `Has`, `Can` prefix | `IsActive`, `HasRecords`, `CanAccess` |

---

## Code Organization

**Standards:**
- 4 spaces indentation (no tabs)
- Allman braces (opening brace on new line)
- 100-120 character line length
- One public type per file
- Using statements: alphabetical, System first
- Member order: Fields → Constructors → Public Methods → Private Methods

---

## Repository Pattern

All data access must use repository interfaces:

```csharp
// Interface
public interface IStudentRepository
{
    Task<Student?> GetByIdAsync(int id);
    Task<List<Student>> GetAllAsync();
    Task AddAsync(Student student);
}

// Usage in controller
public class StudentController : Controller
{
    private readonly IStudentRepository _repository;
    
    public StudentController(IStudentRepository repository)
    {
        _repository = repository;
    }
}
```

**Why:** Testability, loose coupling, consistency

---

## Dependency Injection

```csharp
// Program.cs
builder.Services.AddScoped<IStudentRepository, DbStudentRepository>();
builder.Services.AddScoped<IAttendanceRepository, DbAttendanceRepository>();
```

**Lifetimes:**
- **Scoped:** Per HTTP request (Repositories, DbContext)
- **Transient:** Every time created (lightweight utilities)
- **Singleton:** Application lifetime (configuration)

---

## Async/Await

```csharp
// ✅ Async for I/O operations
public async Task<Student?> GetStudentByIdAsync(int id)
{
    return await _context.Students.FindAsync(id);
}

// ❌ Avoid: Blocking calls
public Student? GetStudentById(int id)
{
    return _studentRepository.GetByIdAsync(id).Result;  // Blocks!
}
```

**Rules:**
- Method names end with `Async`
- Return `Task` or `Task<T>` (not `void`)
- Use `await` on async operations

---

## Documentation

```csharp
/// <summary>
/// Retrieves a student by their unique identifier.
/// </summary>
/// <param name="studentId">The unique ID of the student.</param>
/// <returns>A Student object if found; otherwise null.</returns>
public Student? GetStudentById(int studentId)
{
    // Explain WHY, not WHAT
    // Document non-obvious logic only
}
```

**Public members must have XML documentation. Comments explain intent, not code.**

---

## Error Handling & Validation

```csharp
if (!ModelState.IsValid)
    return View(student);

try
{
    await _repository.AddAsync(student);
    return Ok();
}
catch (InvalidOperationException ex)
{
    return BadRequest(new { error = ex.Message });
}
```

---

## Code Review Checklist

- [ ] Follows naming conventions
- [ ] Properly documented (public members)
- [ ] Uses repository pattern
- [ ] Async for I/O operations
- [ ] Appropriate error handling
- [ ] No code duplication

---

## Quick Reference

| Pattern | Example |
|---------|---------|
| **Nullable Reference Types** | `public string? MiddleName { get; set; }` |
| **Include for Related Data** | `Include(s => s.AttendanceRecords)` |
| **Async Database Query** | `await _context.Students.ToListAsync()` |
| **Method Naming** | `GetStudentById`, `AddRecordAsync` |

---

**Last Updated:** November 2025  
**Version:** 1.0.0
