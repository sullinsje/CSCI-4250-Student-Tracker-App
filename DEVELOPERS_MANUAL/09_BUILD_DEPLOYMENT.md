# Build and Deployment

**Build process, database management, configuration, and deployment procedures.**

---

## Build Workflow

```powershell
# Clean and restore
dotnet clean
dotnet restore

# Build
dotnet build                    # Debug build
dotnet build -c Release         # Production build

# Database
dotnet ef database update --context ApplicationDbContext

# Run
dotnet run
```

---

## Database Management

**File:** `StudentTrackerApp/Data/Data.db` (SQLite)

### Initial Setup
```powershell
# Apply migrations (creates database)
dotnet ef database update --context ApplicationDbContext
```

### Creating Migrations

```powershell
# 1. Update entity class
# 2. Create migration
dotnet ef migrations add AddStudentPhoneNumber --context ApplicationDbContext

# 3. Review migration file
# 4. Apply to database
dotnet ef database update --context ApplicationDbContext
```

### Viewing Database

```powershell
# List all migrations
dotnet ef migrations list --context ApplicationDbContext

# Reset database (development only)
Remove-Item StudentTrackerApp\Data\Data.db
dotnet ef database update --context ApplicationDbContext
```

---

## Configuration

**Files:**
- `appsettings.json` - Production settings
- `appsettings.Development.json` - Dev overrides
- `Properties/launchSettings.json` - Launch profiles

### appsettings.json
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=Data.db"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Warning"
    }
  }
}
```

### Running with Different Profiles
```powershell
dotnet run                           # HTTP (default)
dotnet run --launch-profile https    # HTTPS
```

---

## Deployment

### Pre-Deployment
- [ ] Code reviewed and merged to `main`
- [ ] All tests passing
- [ ] Database migrations tested locally
- [ ] `appsettings.json` configured for environment
- [ ] No secrets committed

### Release Build
```powershell
dotnet publish -c Release -o ./Release
```

### Deploy Steps

1. **Backup current system**
   ```powershell
   Copy-Item ./StudentTrackerApp ./StudentTrackerApp.backup -Recurse
   Copy-Item ./Data/Data.db ./Data/Data.db.backup
   ```

2. **Deploy application**
   ```powershell
   Copy-Item ./Release/* C:\Production -Recurse -Force -Exclude Data.db
   ```

3. **Apply migrations**
   ```powershell
   dotnet ef database update --context ApplicationDbContext
   ```

4. **Restart application**
   ```powershell
   Stop-Process -Name StudentTrackerApp -ErrorAction SilentlyContinue
   ./StudentTrackerApp.exe
   ```

5. **Verify**
   - Test login
   - Check database access
   - Verify API endpoints

### Rollback
```powershell
Copy-Item ./StudentTrackerApp.backup ./StudentTrackerApp -Recurse -Force
Copy-Item ./Data/Data.db.backup ./Data/Data.db -Force
./StudentTrackerApp.exe
```

---

## Common Commands Reference

```powershell
# Build
dotnet build                         # Debug
dotnet build -c Release              # Production

# Run
dotnet run                           # Start app
dotnet run --launch-profile https    # HTTPS

# Publish
dotnet publish -c Release -o ./Release

# Database
dotnet ef migrations add <Name>      # Create migration
dotnet ef database update            # Apply migrations
dotnet ef migrations list            # List all migrations

# NuGet
dotnet restore                       # Restore packages
dotnet add package <PackageName>     # Add package

# Cleanup
dotnet clean                         # Remove build artifacts
```

---

## Troubleshooting

**Port Already in Use:**
```powershell
netstat -ano | findstr :5013        # Find process
taskkill /PID <PID> /F              # Kill process
```

**Database Locked:**
```powershell
# Stop application (Ctrl+C)
Remove-Item StudentTrackerApp\Data\Data.db
dotnet ef database update --context ApplicationDbContext
```

**Build Fails - Missing Files:**
```powershell
Remove-Item obj -Recurse -Force
dotnet restore
dotnet build
```

---

## Related Documents

- [02_ENVIRONMENT_SETUP.md](02_ENVIRONMENT_SETUP.md) - Initial setup
- [05_DATA_DICTIONARY.md](05_DATA_DICTIONARY.md) - Database schema
- [11_TROUBLESHOOTING.md](11_TROUBLESHOOTING.md) - Common issues

---

**Last Updated:** November 2025  
**Version:** 1.0.0
