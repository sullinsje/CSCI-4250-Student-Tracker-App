# Development Environment & Setup

**Complete guide for setting up your development environment and getting the project running locally.**

---

## Table of Contents

- [System Requirements](#system-requirements)
- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Database Initialization](#database-initialization)
- [IDE Configuration](#ide-configuration)
- [Verification](#verification)

---

## System Requirements

- **Operating System:** Windows 10+ (or macOS/Linux with .NET SDK)
- **.NET SDK:** Version 9.0 or later
- **Visual Studio:** 2022 or later (recommended) OR VS Code with C# extension
- **PowerShell:** 5.1 or PowerShell Core (for running commands)
- **SQLite:** Built into .NET (no separate installation needed)
- **Git:** Version control system
- **RAM:** Minimum 4GB (8GB recommended)
- **Disk Space:** 2GB for .NET SDK + project files

---

## Prerequisites

### Step 1: Install .NET 9 SDK

1. Visit [dotnet.microsoft.com/download/dotnet/9.0](https://dotnet.microsoft.com/download/dotnet/9.0)
2. Download the SDK for your operating system
3. Run the installer and follow the prompts
4. Verify installation:
   ```powershell
   dotnet --version
   ```
   Should display version 9.0.x

### Step 2: Install Visual Studio or VS Code

**Option A: Visual Studio 2022 (Recommended)**
1. Download [Visual Studio 2022 Community Edition](https://visualstudio.microsoft.com/downloads/)
2. During installation, select workload: **ASP.NET and web development**
3. Also select: **Desktop development with C++** and **.NET Desktop development**

**Option B: VS Code (Lightweight)**
1. Download [Visual Studio Code](https://code.visualstudio.com/)
2. Install these extensions:
   - C# (by Microsoft)
   - .NET Extension Pack (by Microsoft)
   - SQL Server (mssql) for database inspection

### Step 3: Install Git

1. Download [Git for Windows](https://git-scm.com/download/win)
2. Use default installation options
3. Verify: `git --version`

---

## Installation

### Clone the Repository

```powershell
# Navigate to where you want the project
cd C:\Users\YourUsername\Documents

# Clone the repository
git clone https://github.com/sullinsje/CSCI-4250-Student-Tracker-App.git

# Navigate into the project
cd CSCI-4250-Student-Tracker-App\StudentTrackerApp
```

### Install Dependencies

```powershell
# Restore NuGet packages
dotnet restore

# Verify build succeeds
dotnet build
```

If build succeeds, you'll see:
```
Build succeeded.
```

---

## Database Initialization

### First-Time Database Setup

The application uses SQLite with Entity Framework Core. The database file is automatically created on first run.

```powershell
# Apply database migrations (creates Data/Data.db)
dotnet ef database update --context ApplicationDbContext
```

**What happens:**
1. SQLite database file created at `StudentTrackerApp/Data/Data.db`
2. All pending migrations applied to database schema
3. Three user roles created: Admin, Teacher, Student

### Database File Location

- **Path:** `StudentTrackerApp/Data/Data.db`
- **Size:** Starts ~100KB (grows with data)
- **Access:** Can inspect with VS Code SQLite extension or command-line tools

### Reset Database (Development Only)

⚠️ **Warning:** This deletes all data. Only do this in development!

```powershell
# Remove database file
Remove-Item StudentTrackerApp\Data\Data.db

# Recreate from migrations
dotnet ef database update --context ApplicationDbContext
```

---

## IDE Configuration

### Visual Studio 2022

#### Setup

1. **Open Solution**
   - File → Open → Project/Solution
   - Navigate to `StudentTrackerApp.sln`
   - Click Open

2. **Set Startup Project**
   - Right-click on **StudentTrackerApp** in Solution Explorer
   - Select "Set as Startup Project"

3. **Configure Launch Settings**
   - Project → StudentTrackerApp Properties
   - Debug tab
   - Verify "Use the current project" is selected

#### Running the Application

- **Without Debugging:** `Ctrl+F5`
- **With Debugging:** `F5`
- **Output Window:** View → Output (to see console logs)

#### Useful Shortcuts

| Shortcut | Action |
|----------|--------|
| `Ctrl+F5` | Run without debugging |
| `F5` | Run with debugging |
| `Ctrl+Shift+B` | Build solution |
| `Ctrl+K, Ctrl+C` | Comment selected code |
| `Ctrl+K, Ctrl+U` | Uncomment selected code |
| `Ctrl+H` | Find and replace |
| `F6` | Build project |

### VS Code

#### Setup

1. **Open Workspace**
   ```powershell
   code C:\path\to\CSCI-4250-Student-Tracker-App
   ```

2. **Install C# Extension**
   - Extensions (Ctrl+Shift+X)
   - Search "C# Dev Kit"
   - Click Install

3. **Create Launch Configuration**
   - Run → Add Configuration
   - Select ".NET"
   - Automatically creates `.vscode/launch.json`

#### Running the Application

- **Start Debugging:** `F5`
- **Stop Debugging:** `Shift+F5`
- **Build:** `Ctrl+Shift+B`

#### Debug Console

View debug output:
- **Run → Open Debug Console** or press `Ctrl+Shift+Y`

#### Terminal Integration

- **View → Terminal** or `Ctrl+` (backtick)
- Run commands directly in VS Code terminal

#### Recommended VS Code Settings

Add to `.vscode/settings.json`:

```json
{
    "[csharp]": {
        "editor.defaultFormatter": "ms-dotnettools.csharp",
        "editor.formatOnSave": true
    },
    "editor.rulers": [100, 120],
    "files.exclude": {
        "bin/": true,
        "obj/": true
    }
}
```

---

## Verification

### Check Everything Works

1. **Build Successfully**
   ```powershell
   dotnet build
   ```
   Look for: `Build succeeded.`

2. **Database Exists**
   ```powershell
   Test-Path StudentTrackerApp\Data\Data.db
   # Should return: True
   ```

3. **Run Application**
   ```powershell
   dotnet run
   ```
   Should display:
   ```
   info: Microsoft.Hosting.Lifetime[14]
       Now listening on: http://localhost:5013
   ```

4. **Open in Browser**
   - Automatically opens to `http://localhost:5013`
   - Or manually navigate to that URL

5. **Test Authentication**
   - Click "Register Student"
   - Create a test account (email: `test@example.com`, password: `Test123!`)
   - Should successfully register and redirect to dashboard

### Troubleshooting Initial Setup

**Problem:** "The type or namespace name 'Student' does not exist"
- **Solution:** Run `dotnet restore` and rebuild

**Problem:** Cannot create database migration
- **Solution:** Ensure .NET 9 SDK is installed (`dotnet --version`)

**Problem:** Port 5013 already in use
- **Solution:** Change port in `launchSettings.json` or kill process using port

For more help, see [11_TROUBLESHOOTING.md](11_TROUBLESHOOTING.md)

---

## Development Tools (Recommended)

### Essential

- **Visual Studio 2022 Community** - Full-featured IDE (Free)
- **Git Bash** - Command-line Git tools
- **PowerShell Core** - Cross-platform shell

### Optional but Useful

- **VS Code** - Lightweight code editor
- **Postman** or **Insomnia** - API testing
- **Git Desktop** - Visual Git client
- **SQLite Browser** - Database inspection
- **Swagger UI** - Interactive API docs (built-in via NSwag)

---

## Next Steps

1. **Read the Architecture Guide:** [06_ARCHITECTURE_DESIGN.md](06_ARCHITECTURE_DESIGN.md)
2. **Review Coding Standards:** [03_CODING_STANDARDS.md](03_CODING_STANDARDS.md)
3. **Understand Development Workflow:** [04_DEVELOPMENT_STANDARDS.md](04_DEVELOPMENT_STANDARDS.md)

---

**Last Updated:** November 2025  
**Version:** 1.0.0
