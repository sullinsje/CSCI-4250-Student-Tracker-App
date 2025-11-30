# Design Decisions

**Rationale behind key technical and architectural decisions.**

---

## Database: SQLite

**Why:** File-based, zero configuration, perfect for MVP  
**Advantages:**
- No server installation or management
- Single portable database file
- Built-in EF Core support
- Sufficient for 1000-10,000 concurrent users

**Scalability Path:** If growing, swap provider (PostgreSQL, SQL Server) with minimal code changes due to ORM abstraction.

---

## Repository Pattern

**Why:** Abstraction layer for all data access  
**Benefits:**
- Unit tests can mock repositories (test business logic without database)
- Loose coupling between controllers and database
- Standardized data access patterns
- Easy to add caching or change persistence layer

```csharp
// Controllers depend on abstraction, not implementation
public class StudentController : Controller
{
    private readonly IStudentRepository _repository;  // Interface, not DbContext
}
```

---

## Geolocation: Raw Coordinates

**Why:** Store latitude/longitude, not addresses  
**Advantages:**
- Accurate (no geocoding API dependency)
- Efficient (8 bytes vs 50+ bytes for strings)
- Flexible (can reverse-geocode later if needed)
- No external API calls

**Display to User:**
```html
<a href="https://maps.google.com/?q=36.304516,-82.412345" target="_blank">
    View on Map
</a>
```

---

## Role-Based Authorization

**Why:** Simple role structure (Admin, Teacher, Student) is sufficient for current needs  
**Structure:**
- **Admin:** User management
- **Teacher:** View all students and attendance
- **Student:** Clock in/out, view own attendance

**Future:** Can add claims-based authorization for granular permissions if needed.

---

## DateOnly Type

**Why:** Use DateOnly (not DateTime) for attendance dates  
**Rationale:**
- Semantically correct (attendance tracked per day, not per time)
- Prevents timezone complexity
- Cleaner database schema
- Available in .NET 6+ (project uses .NET 9)

---

## Async/Await for I/O

**Why:** All database and network operations are async  
**Benefits:**
- Thread pool efficiency (threads released during I/O waits)
- Better scalability
- Responsive UI

**Standard Pattern:**
```csharp
// ✅ Async for I/O
public async Task<Student?> GetStudentByIdAsync(int id)
{
    return await _context.Students.FindAsync(id);
}

// ❌ Avoid blocking
public Student? GetStudentById(int id)
{
    return _studentRepository.GetByIdAsync(id).Result;  // Blocks thread!
}
```

---

## Separate API Endpoints

**Why:** Include REST API alongside MVC views  
**Rationale:**
- Current frontend: Server-rendered Razor views
- Future flexibility: React/Angular frontend can use APIs
- Mobile app integration
- Third-party integration capability
- Future-proofs the backend

**Current Endpoints:**
- `POST /api/students/{id}/attendance` - Add attendance record
- `GET /api/users/{id}` - Get user info

---

## Trade-offs Summary

| Aspect | Current Choice | When Scaling | Migration Path |
|--------|---|---|---|
| **Database** | SQLite | 10,000+ users | Change connection string + provider |
| **Authorization** | Role-based | Fine-grained | Add claims-based layer |
| **Caching** | None | High traffic | Add Redis |
| **API** | Basic | Many clients | Add versioning + documentation |

All current decisions support these future transitions.

---

## Related Documents

- [05_DATA_DICTIONARY.md](05_DATA_DICTIONARY.md) - Database schema
- [03_CODING_STANDARDS.md](03_CODING_STANDARDS.md) - Implementation patterns

---

**Last Updated:** November 2025  
**Version:** 1.0.0
