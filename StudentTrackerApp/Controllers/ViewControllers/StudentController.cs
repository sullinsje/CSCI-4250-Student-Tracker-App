using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using StudentTrackerApp.Models;

namespace StudentTracker.Controllers;

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
        //var data = _dbContext.AttendanceRecords.ToList();
        return View();
    }

    public IActionResult Login()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}