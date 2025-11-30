# Development Standards

**File organization, git workflow, branch strategy, and code review process.**

---

## File Organization

```
StudentTrackerApp/
├── Controllers/          # HTTP request handlers
├── Views/               # Razor templates (organized by role)
├── Models/
│   ├── Entities/        # Domain entities (Student, AttendanceRecord)
│   └── LoginModel.cs
├── Services/            # Data access & business logic
│   ├── ApplicationDbContext.cs
│   ├── IStudentRepository.cs
│   └── DbStudentRepository.cs
├── Migrations/          # Entity Framework migrations
├── Data/
│   └── Data.db          # SQLite database
└── wwwroot/             # Static files (CSS, JS, images)
```

**Adding Features:**
- New entity → `Models/Entities/`
- New views → `Views/{RoleName}/`
- New repository → `Services/I{Name}Repository.cs` + `Services/Db{Name}Repository.cs`

---

## Branch Strategy

| Branch Type | Pattern | Purpose | Base | Merge To |
|-----------|---------|---------|------|----------|
| Feature | `feature/*` | New functionality | `main` or `develop` | PR → `develop` |
| Bugfix | `bugfix/*` | Bug fixes | `main` | PR → `develop` |
| Hotfix | `hotfix/*` | Critical prod bug | `main` | PR → `main` + `develop` |

**Branch Naming:** Descriptive and kebab-case
- ✅ `feature/add-geolocation-map`
- ❌ `feature/new-stuff` (too vague)

---

## Commit Messages

Follow Conventional Commits:

```
<type>(<scope>): <subject>

<body>
```

**Types:** `feat`, `fix`, `docs`, `style`, `refactor`, `test`, `chore`, `perf`

**Example:**
```
feat(attendance): add CSV export

Implements export of attendance records to CSV format
for bulk analysis and reporting.

Closes #42
```

**Rules:**
- Imperative mood ("add" not "added")
- No period at end
- Max 50 characters for subject
- Explain WHAT and WHY in body

---

## Pull Request Workflow

1. **Create feature branch** from `develop`
2. **Make commits** with clear messages
3. **Push** to remote
4. **Create PR** with description and checklist
5. **Code review** (2+ approvals required)
6. **Address feedback** and push updates
7. **Squash and merge** when approved

**Pre-Review Checklist:**
- [ ] Branch is up to date with `develop`
- [ ] Code compiles, no warnings
- [ ] Follows coding standards
- [ ] Database migrations tested
- [ ] No debug code or secrets

---

## Code Review

**Reviewers Check:**
- ✅ Logic & functionality
- ✅ Code quality & maintainability
- ✅ Performance concerns (N+1 queries?)
- ✅ Security (authorization checks?)
- ✅ Test coverage

**Constructive Feedback:**
```csharp
// Good
This loads all records then filters. Consider moving 
the filter to the database query for performance.

// Avoid
This is wrong. Don't do this.
```

---

## Common Workflow

```powershell
# Create feature branch
git checkout -b feature/add-export

# Make changes and commit
git add .
git commit -m "feat(attendance): add export service"

# Push to remote
git push origin feature/add-export

# On GitHub: Create PR, request reviewers

# After approval, merge on GitHub
# Then clean up locally
git checkout develop
git pull origin develop
git branch -d feature/add-export
```

---

## Repository Structure

- **Repository:** https://github.com/sullinsje/CSCI-4250-Student-Tracker-App
- **Default Branch:** `main`
- **Development Branch:** `develop` (if used)

---

## Related Documents

- [03_CODING_STANDARDS.md](03_CODING_STANDARDS.md) - Code style
- [05_DATA_DICTIONARY.md](05_DATA_DICTIONARY.md) - Database schema
- [09_BUILD_DEPLOYMENT.md](09_BUILD_DEPLOYMENT.md) - Deployment after merge

---

**Last Updated:** November 2025  
**Version:** 1.0.0
