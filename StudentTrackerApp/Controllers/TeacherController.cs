using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace StudentTracker.Controllers;

public class TeacherController : Controller
{
    private readonly ILogger<TeacherController> _logger;

    public TeacherController(ILogger<TeacherController> logger)
    {
        _logger = logger;
    }

    public IActionResult Teacher()
    {
        return View();
    }

    public IActionResult StudentList()
    {
        //var data = _dbContext.AttendanceRecords.ToList();
        return View();
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