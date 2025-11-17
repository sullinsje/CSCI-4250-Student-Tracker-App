using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Identity;
using StudentTrackerApp.Models;
using StudentTrackerApp.Models.Entities;
using StudentTrackerApp.Services;

namespace StudentTrackerApp.Controllers;

public class StudentController : Controller
{
    private readonly ILogger<StudentController> _logger;
    private readonly ApplicationDbContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public StudentController(ILogger<StudentController> logger, ApplicationDbContext db, UserManager<ApplicationUser> userManager)
    {
        _logger = logger;
        _db = db;
        _userManager = userManager;
    }

    public IActionResult Student()
    {
        return View();
    }

    public IActionResult AttendanceHistory()
    {
        var userId = _userManager.GetUserId(User);

        var student = _db.Students
            .FirstOrDefault(s => s.IdentityUserId == userId);
        
        if (student == null)
        {
            return NotFound();
        }

        var records = _db.AttendanceHistory
            .Where(a => a.StudentId == student.id)
            .OrderByDescending(a => a.ClockInTime)
            .ToList();

        return View(records);
    }
    

    public IActionResult Login()
    {
        return View();
    }

    public IActionResult Dashboard()
    {
        return View();
    }

    public IActionResult Logout()
    {
        return RedirectToAction("Home", "Home");
    }
    // Temporary authentication: User:12345 Pass: password
    [HttpPost]
        public IActionResult Login(string studentId, string password, bool remember)
        {
            
            if (string.IsNullOrEmpty(studentId) || string.IsNullOrEmpty(password))
            {
                ViewBag.Error = "Please enter both Student ID and password.";
                return View();
            }

            if (studentId == "12345" && password == "password")
            {
                return RedirectToAction("Dashboard", "Student");
            }
            else
            {
                ViewBag.Error = "Invalid Student ID or password.";
                return View();
            }
        }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

     // POST: Student/RecordAttendance
        [HttpPost]
        public IActionResult ClockIn([FromBody] LocationData data)
        {
            var record = new AttendanceHistory
            {
                ClockInLatitude = data.Latitude,
                ClockInLongitude = data.Longitude,
                ClockInTime = DateOnly.FromDateTime(DateTime.Now)
            };

            _db.AttendanceHistory.Add(record);
            _db.SaveChanges();

            return Ok();
        }
        
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}