using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentTrackerApp.Services;
using StudentTrackerApp.Models;
using StudentTrackerApp.Models.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace StudentTracker.Controllers;

[Authorize(Roles = "Teacher")]
public class TeacherController : Controller
{
    private readonly ILogger<TeacherController> _logger;

    private readonly IAttendanceRepository _attendanceRepo;
    private readonly IStudentRepository _studentRepo;

    public TeacherController(ILogger<TeacherController> logger, IAttendanceRepository attendanceRepo, IStudentRepository studentRepo)
    {
        _logger = logger;
        _attendanceRepo = attendanceRepo;
        _studentRepo = studentRepo;
    }

    public IActionResult Teacher()
    {
        return View();
    }

    public async Task<IActionResult> StudentList()
    {
        var students = await _studentRepo.ReadAllAsync();
        return View(students);
    }

    // GET: Teacher/AttendaceHistory/5
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

    public IActionResult Login()
    {
        return View();
    }

    public IActionResult Dashboard()
    {
        return View();
    }

    // public IActionResult Error()
    // {
    //     return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    // }
}