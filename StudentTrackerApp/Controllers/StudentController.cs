using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using StudentTrackerApp.Models;

namespace StudentTrackerApp.Controllers;

[Authorize(Roles = "Student")]
public class StudentController : Controller
{
    private readonly ILogger<StudentController> _logger;

    public StudentController(ILogger<StudentController> logger)
    {
        _logger = logger;
    }

    public IActionResult Student()
    {
        return View();
    }

    public IActionResult AttendanceHistory()
    {
        var sampleAttendance = new List<Attendance>();

        return View(sampleAttendance);
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
    public IActionResult RecordAttendance(double latitude, double longitude, string clockType)
    {
        // TODO: Save the attendance record to database
        // For now, just log it
        var studentId = "12345"; // TODO: Get from session/authentication
        var currentTime = DateTime.Now;

        Console.WriteLine($"Student {studentId} clocked {clockType} at {currentTime}");
        Console.WriteLine($"Location: {latitude}, {longitude}");

        // TODO: Save to database
        // var attendance = new Attendance
        // {
        //     StudentId = studentId,
        //     Date = currentTime.Date,
        //     ClockInTime = currentTime.TimeOfDay,
        //     Latitude = latitude,
        //     Longitude = longitude
        // };
        // Save attendance to database

        TempData["SuccessMessage"] = $"Successfully clocked {clockType}!";
        return RedirectToAction("Student");
    }
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}