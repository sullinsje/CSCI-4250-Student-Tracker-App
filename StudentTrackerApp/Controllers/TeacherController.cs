using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentTrackerApp.Services;
using StudentTrackerApp.Models.Entities;

namespace StudentTracker.Controllers;

/// <summary>
/// Controller for teacher-facing pages and actions.
/// Provides endpoints to view the teacher dashboard, list students, and
/// view attendance history for an individual student.
/// </summary>
[Authorize(Roles = "Teacher")]
public class TeacherController : Controller
{
    private readonly ILogger<TeacherController> _logger;

    private readonly IAttendanceRepository _attendanceRepo;
    private readonly IStudentRepository _studentRepo;

    /// <summary>
    /// Creates a new <see cref="TeacherController"/>.
    /// </summary>
    /// <param name="logger">Logger instance for the controller.</param>
    /// <param name="attendanceRepo">Repository for attendance records.</param>
    /// <param name="studentRepo">Repository for student data.</param>
    public TeacherController(ILogger<TeacherController> logger, IAttendanceRepository attendanceRepo, IStudentRepository studentRepo)
    {
        _logger = logger;
        _attendanceRepo = attendanceRepo;
        _studentRepo = studentRepo;
    }

    /// <summary>
    /// Landing action that returns the teacher landing page.
    /// </summary>
    public IActionResult Teacher()
    {
        return View();
    }

    /// <summary>
    /// Returns a view containing the list of all students for the teacher.
    /// </summary>
    /// <returns>A view with an <see cref="IEnumerable{Student}"/> model.</returns>
    public async Task<IActionResult> StudentList()
    {
        var students = await _studentRepo.ReadAllAsync();
        return View(students);
    }

    // GET: Teacher/AttendaceHistory/5
    /// <summary>
    /// Shows attendance history for a specific student.
    /// </summary>
    /// <param name="id">The student's Id.</param>
    /// <returns>A view with the attendance records for the student.</returns>
    public async Task<IActionResult> AttendaceHistory(int id)
    {
        // Read attendance records for the student (entity AttendanceRecord)
        var records = await _attendanceRepo.ReadAllByStudentAsync(id);

        // Map entity AttendanceRecord -> view model StudentTrackerApp.Models.Attendance
        var model = records.Select(r => new AttendanceRecord
        {
            Id = r.Id,
            StudentId = r.StudentId,
            Date = r.Date,
            ClockInLatitude = r.ClockInLatitude,
            ClockInLongitude = r.ClockInLongitude,
            ClockType = r.ClockType
        }).ToList();

        // Return the view named exactly as the file: "AttendaceHistory"
        return View("AttendaceHistory", model);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

    /// <summary>
    /// Returns the teacher dashboard view.
    /// </summary>
    public IActionResult Dashboard()
    {
        return View();
    }
    

}