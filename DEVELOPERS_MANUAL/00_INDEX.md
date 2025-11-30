# Developer's Manual - Student Tracker Application

**Version:** 1.0.0  
**Last Updated:** November 2025  
**Framework:** ASP.NET Core MVC (.NET 9)  
**Database:** SQLite with Entity Framework Core

---

## Quick Links

This manual is organized into focused topic files for easier navigation. Select the topic you need:

| Document | Purpose |
|----------|---------|
| [01_GLOSSARY.md](01_GLOSSARY.md) | Essential terminology and definitions |
| [02_ENVIRONMENT_SETUP.md](02_ENVIRONMENT_SETUP.md) | Development environment configuration and installation |
| [03_CODING_STANDARDS.md](03_CODING_STANDARDS.md) | C# code style, naming conventions, and best practices |
| [04_DEVELOPMENT_STANDARDS.md](04_DEVELOPMENT_STANDARDS.md) | File organization, git workflow, code review process |
| [05_DATA_DICTIONARY.md](05_DATA_DICTIONARY.md) | Database schema, data models, and relationships |
| [06_ARCHITECTURE_DESIGN] | https://github.com/sullinsje/CSCI-4250-Student-Tracker-App/blob/main/ArchDesign.png
| [07_TEST_PROCESS.md](07_TEST_PROCESS.md) | Testing strategies, checklists, and implementation |
| [08_ISSUE_TRACKING.md](08_ISSUE_TRACKING.md) | Issue management, project tools, and communication |
| [08a project management board] circa 11/30/25 | https://drive.google.com/file/d/12hAxwXabXx9XRXSVgFWf3UALQFnuCNP8/view?usp=sharing
| [09_BUILD_DEPLOYMENT.md](09_BUILD_DEPLOYMENT.md) | Build process, deployment procedures, configuration |
| [10_DESIGN_DECISIONS.md](10_DESIGN_DECISIONS.md) | Rationale behind key architectural choices |
| [11_TROUBLESHOOTING.md](11_TROUBLESHOOTING.md) | Common issues and solutions organized by category |

---

## Getting Started

**New to the project?** Start here:

1. Read [02_ENVIRONMENT_SETUP.md](02_ENVIRONMENT_SETUP.md) to set up your development environment
2. Review [06_ARCHITECTURE_DESIGN.md](06_ARCHITECTURE_DESIGN.md) to understand the application structure
3. Check [03_CODING_STANDARDS.md](03_CODING_STANDARDS.md) before writing your first line of code

**Facing an issue?** Jump to [11_TROUBLESHOOTING.md](11_TROUBLESHOOTING.md)

**Need to deploy?** See [09_BUILD_DEPLOYMENT.md](09_BUILD_DEPLOYMENT.md)

---

## Quick Reference Commands

```powershell
# Build and Run
dotnet build                              # Compile project
dotnet run                                # Run application

# Database
dotnet ef migrations add <Name>           # Create new migration
dotnet ef database update                 # Apply pending migrations

# NuGet
dotnet restore                            # Restore packages
dotnet add package <PackageName>          # Add new package
```

For more commands, see [09_BUILD_DEPLOYMENT.md](09_BUILD_DEPLOYMENT.md).

---

## Document Structure

Each document is self-contained but linked where relevant. Use Ctrl+Click to navigate between documents in VS Code or your IDE.

**Maintained By:** Development Team  
**Repository:** https://github.com/sullinsje/CSCI-4250-Student-Tracker-App  
**Next Review Date:** Q2 2026
