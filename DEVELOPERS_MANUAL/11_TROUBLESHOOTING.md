# Troubleshooting Guide

**Solutions for common issues: Build, Database, Authentication, Runtime, and Deployment.**

---

## Build & Compilation

**Problem:** "The type or namespace name 'Student' does not exist"

**Solution:** Add using statement
```csharp
using StudentTrackerApp.Models.Entities;
using StudentTrackerApp.Services;
```

---

**Problem:** NuGet Package Restore Fails

**Solution:**
```powershell
dotnet nuget locals all --clear
dotnet restore
dotnet build
```

---

**Problem:** Build Succeeds but Editor Shows Red Squiggles (IntelliSense Cache)

**Solution:**
```powershell
Remove-Item .\.vscode -Recurse -Force
# Reopen VS Code and reinstall C# extension
```

---

## Database & Migrations

**Problem:** "There is already an open DataReader associated with this connection"

**Cause:** Multiple concurrent queries on same DbContext

**Solution:** Use async properly
```csharp
// ✅ Correct
var students = await _context.Students.ToListAsync();
var records = await _context.AttendanceRecords.ToListAsync();

// ❌ Wrong (blocks)
var students = _context.Students.ToList();
var records = _context.AttendanceRecords.ToList();
```

---

**Problem:** Database File Locked (Cannot Delete `Data.db`)

**Solution:**
```powershell
# Stop running application (Ctrl+C)
Remove-Item StudentTrackerApp\Data\Data.db
dotnet ef database update --context ApplicationDbContext
```

---

**Problem:** Foreign Key Constraint Violation

**Solution:** Validate referenced entity exists
```csharp
var student = await _context.Students.FindAsync(studentId);
if (student == null)
    throw new InvalidOperationException("Student not found");

var record = new AttendanceRecord
{
    StudentId = student.Id,  // Valid reference
    // ... other properties
};
```

---

**Problem:** Migrations Not Being Applied

**Solution:**
```powershell
# 1. Check migration file exists
Get-ChildItem Migrations\*.cs

# 2. List pending migrations
dotnet ef migrations list --context ApplicationDbContext

# 3. Apply pending
dotnet ef database update --context ApplicationDbContext
```

---

## Authentication & Authorization

**Problem:** User Can Access Pages They Shouldn't

**Solution:** Add `[Authorize]` attribute
```csharp
[Authorize(Roles = "Student")]
public class StudentController : Controller { }

[Authorize(Roles = "Teacher")]
public IActionResult AttendanceHistory(int studentId) { }
```

---

**Problem:** "Object reference not set" When Accessing User.Identity

**Solution:** Check for null
```csharp
var userId = User?.FindFirstValue(ClaimTypes.NameIdentifier);
if (string.IsNullOrEmpty(userId))
    return Unauthorized();
```

---

**Problem:** User Stays Logged In After Logout

**Solution:** Clear session in logout action
```csharp
public async Task<IActionResult> Logout()
{
    await _signInManager.SignOutAsync();
    HttpContext.Session.Clear();
    return RedirectToAction("Index", "Home");
}
```

---

## Runtime Issues

**Problem:** Geolocation Not Captured (Coordinates are 0,0)

**Solution Checklist:**
- [ ] Running on localhost or HTTPS (not plain HTTP)
- [ ] Browser allows location permission (check URL bar lock icon)
- [ ] Device has GPS or IP-based location
- [ ] JavaScript is sending coordinates to server

**Debug:**
```html
<script>
navigator.geolocation.getCurrentPosition(
    p => console.log("Location:", p),
    e => console.error("Error:", e)
);
</script>
```

---

**Problem:** Attendance Record Not Saving to Database

**Solution:** Ensure SaveChangesAsync() is called
```csharp
_context.AttendanceRecords.Add(record);
await _context.SaveChangesAsync();  // Must call this!

// Verify it saved
var saved = await _context.AttendanceRecords.FindAsync(record.Id);
if (saved == null)
    throw new InvalidOperationException("Record not saved!");
```

---

**Problem:** Page Loads Slowly (N+1 Queries)

**Solution:** Eager load related data
```csharp
// ❌ Slow: Extra query per student
var students = _context.Students.ToList();
foreach (var s in students)
{
    var records = _context.AttendanceRecords  // Extra query!
        .Where(r => r.StudentId == s.Id).ToList();
}

// ✅ Fast: Load all at once
var students = await _context.Students
    .Include(s => s.AttendanceRecords)  // Single query
    .ToListAsync();
```

---

## Deployment

**Problem:** Application Works Locally but Not on Server

**Checklist:**
- [ ] Database file exists at configured path
- [ ] Application has read/write permissions to database
- [ ] Connection string uses correct path for OS
- [ ] appsettings.json environment correct

---

**Problem:** CORS Error - "No 'Access-Control-Allow-Origin' Header"

**Solution:** Update CORS policy in `Program.cs`
```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigins",
        policy =>
        {
            policy.WithOrigins("https://example.com")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});
```

---

**Problem:** Port Already in Use

**Solution:**
```powershell
netstat -ano | findstr :5013      # Find process
taskkill /PID <PID> /F            # Kill it
# Or change port in launchSettings.json
```

---

## Getting Help

1. **Search this guide** first
2. **Check GitHub Issues** - https://github.com/sullinsje/CSCI-4250-Student-Tracker-App/issues
3. **Microsoft Docs** - https://learn.microsoft.com/dotnet/core
4. **Stack Overflow** - Tag: `asp.net-core`
5. **Ask code review partner**

### Detailed Bug Report Template

```
**Issue:** [What's wrong?]

**Steps to Reproduce:**
1. 
2. 

**Expected:** [What should happen]

**Actual:** [What happens]

**Environment:**
- OS: Windows/macOS/Linux
- .NET: 9.0
- Browser: Chrome/Firefox

**Error Message:**
[Exact text]
```

---

## Related Documents

- [02_ENVIRONMENT_SETUP.md](02_ENVIRONMENT_SETUP.md) - Setup issues
- [05_DATA_DICTIONARY.md](05_DATA_DICTIONARY.md) - Database reference
- [09_BUILD_DEPLOYMENT.md](09_BUILD_DEPLOYMENT.md) - Deployment issues

---

**Last Updated:** November 2025  
**Version:** 1.0.0  
**Note:** Check back for new solutions (frequently updated)
