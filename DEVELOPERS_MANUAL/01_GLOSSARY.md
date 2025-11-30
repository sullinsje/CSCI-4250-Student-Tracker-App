# Glossary: Important Terms

**Quick reference of essential terminology used throughout the Student Tracker Application.**

---

## Terms & Definitions

| Term | Definition |
|------|-----------|
| **ASP.NET Core MVC** | Microsoft's cross-platform web framework following the Model-View-Controller design pattern |
| **Entity Framework Core (EF Core)** | Object-Relational Mapping (ORM) framework for database operations |
| **DbContext** | EF Core class representing a session with the database; manages entities and change tracking |
| **Migration** | EF Core feature for version-controlling database schema changes |
| **Repository Pattern** | Design pattern abstracting data access logic, providing a collection-like interface |
| **Identity** | ASP.NET Core's built-in authentication and authorization framework |
| **Role-Based Authorization** | Access control mechanism granting permissions based on assigned user roles |
| **Dependency Injection (DI)** | Design pattern for loose coupling by injecting dependencies through constructors |
| **Scoped Service** | DI lifetime where a new instance is created per HTTP request |
| **SQLite** | Lightweight, file-based SQL database (stored as `Data.db`) |
| **CORS** | Cross-Origin Resource Sharing; controls which origins can access the application |
| **Controller** | ASP.NET MVC component handling HTTP requests and returning responses |
| **View** | ASP.NET MVC component (Razor template) rendering HTML for the user |
| **Razor** | ASP.NET templating syntax mixing C# and HTML |
| **API Endpoint** | HTTP interface for programmatic access to application functionality |
| **NSwag** | OpenAPI documentation and code generation tool |
| **Clock In/Clock Out** | Student actions recording location data; true = clock in, false = clock out |
| **DateOnly** | .NET 6+ type representing a date without time component |
| **Geolocation** | Latitude/longitude coordinates capturing student location |
| **ApplicationUser** | Custom Identity user class extending `IdentityUser` |
| **Primary Key (PK)** | Unique identifier column for database records |
| **Foreign Key (FK)** | Column referencing primary key of another table; establishes relationships |
| **One-to-One** | Database relationship where one record in table A links to exactly one record in table B |
| **One-to-Many** | Database relationship where one record in table A links to multiple records in table B |
| **Many-to-Many** | Database relationship where multiple records in table A link to multiple records in table B |
| **Nullable** | Database column that can contain NULL values (no data) |
| **Cascading Delete** | Database behavior automatically deleting related records when parent is deleted |
| **Async/Await** | C# pattern for asynchronous programming without blocking threads |
| **Thread Pool** | Collection of reusable threads managed by .NET runtime |
| **Lazy Loading** | Loading related data only when explicitly accessed |
| **Eager Loading** | Loading related data upfront with main query (using `Include()`) |
| **N+1 Query Problem** | Performance anti-pattern where loading items triggers separate queries for each item |
| **Query Projection** | Selecting specific columns instead of entire entities |
| **Unit Test** | Test of a single method or class in isolation |
| **Integration Test** | Test of multiple components working together |
| **Mock** | Fake object used in tests to simulate behavior |
| **Lambda Expression** | Inline anonymous function using `=>` syntax |
| **LINQ** | Language Integrated Query for filtering/transforming data |
| **Extension Method** | Method added to existing type without modifying original type |
| **Attribute** | Metadata annotation above classes/methods (e.g., `[Authorize]`) |
| **Middleware** | Component in ASP.NET request/response pipeline |
| **Pull Request (PR)** | GitHub feature for proposing code changes before merging |
| **Commit** | Snapshot of code changes saved to version control |
| **Branch** | Isolated line of development in Git |
| **Merge Conflict** | Occurs when Git cannot automatically combine changes from different branches |
| **Code Review** | Team process of examining proposed code changes for quality |
| **Refactoring** | Restructuring code without changing functionality |
| **Technical Debt** | Cost of postponed improvements that accumulates over time |

---

## Related Documents

- [02_ENVIRONMENT_SETUP.md](02_ENVIRONMENT_SETUP.md) - Uses many of these terms in setup instructions
- [03_CODING_STANDARDS.md](03_CODING_STANDARDS.md) - References coding terminology
- [06_ARCHITECTURE_DESIGN.md](06_ARCHITECTURE_DESIGN.md) - Explains architectural patterns

---

**Last Updated:** November 2025  
**Version:** 1.0.0
