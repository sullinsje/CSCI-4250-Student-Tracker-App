using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using StudentTrackerApp.Models.Entities;

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

    public IActionResult Dashboard()
    {
        return View();
    }

    public IActionResult StudentList()
    {
          var sampleStudents = new List<Student>
            {
                new Student
                {
                    id = 12345,
                    name= "John Smith",
                },
                new Student
                {
                    id = 12346,
                    name = "Emily Johnson",
                },
                new Student
                {
                    id = 12347,
                    name = "Michael Brown",
                },
                new Student
                {
                    id = 12348,
                    name = "Sarah Davis",
                },
                new Student
                {
                    id = 12349,
                    name = "James Wilson",
                }
            };
        return View(sampleStudents);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    
    public IActionResult Login()
    {
        return View();
    }
    // public IActionResult Error()
    // {
    //     return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    // }
}