# Test Process

**Testing approach and manual test checklists for key features.**

---

## Setup

```powershell
cd StudentTrackerApp
dotnet build
dotnet ef database update --context ApplicationDbContext
dotnet run
# Application runs on http://localhost:5013
```

---

## Manual Testing Checklist

### Authentication

- [ ] **Register Student:** Can create account and reach student dashboard
- [ ] **Register Teacher:** Can create account and reach teacher dashboard
- [ ] **Login:** Valid credentials work, invalid rejected
- [ ] **Case Insensitivity:** `Student@Test.COM` logs in successfully
- [ ] **Role Authorization:** Student cannot access teacher dashboard (403/redirect)
- [ ] **Logout:** Session clears, protected pages redirect to login

### Student Features

- [ ] **Clock In:** Records current location with geolocation
- [ ] **Clock Out:** Creates separate record with different location
- [ ] **View History:** All past clock records visible, sorted by date
- [ ] **Multiple Same Day:** Can clock in/out multiple times on same day
- [ ] **Google Maps Link:** Coordinates link opens location in new tab

### Teacher Features

- [ ] **View Students:** List of all registered students displays
- [ ] **View Attendance:** Can click student to see all their records
- [ ] **Sorted by Date:** Records show most recent first
- [ ] **View Multiple Clocks:** Shows all in/out records on same day

### Admin Features

- [ ] **Create User:** Can create Student, Teacher, or Admin user
- [ ] **Edit User:** Can change email and role
- [ ] **Delete User:** User removed and cannot login
- [ ] **Cascade Delete:** Student deletion removes all attendance records

### API Endpoints

- [ ] **POST `/api/students/{id}/attendance`:** Creates attendance record
- [ ] **Invalid StudentId:** Returns 404
- [ ] **Invalid Coordinates:** Returns 400 Bad Request

### Data Persistence

- [ ] **Restart Test:** Data persists after stopping and restarting app
- [ ] **Database Integrity:** Foreign keys preserved

---

## Running Tests

```powershell
# All tests
dotnet test

# Specific test file
dotnet test StudentTrackerApp.Tests\StudentRepositoryTests.cs

# With coverage
dotnet test /p:CollectCoverage=true
```

---

## Pre-Release Sign-Off

- [ ] All manual tests passed
- [ ] No build warnings
- [ ] Database migrations applied cleanly
- [ ] Geolocation working in browsers
- [ ] API endpoints responding correctly
- [ ] Authorization enforced properly
- [ ] Data persists after restart

---

## Known Issues

See [11_TROUBLESHOOTING.md](11_TROUBLESHOOTING.md) for common issues.

---

**Last Updated:** November 2025  
**Version:** 1.0.0
