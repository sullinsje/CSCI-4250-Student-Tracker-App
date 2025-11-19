using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentTrackerApp.Models;
using StudentTrackerApp.Services;

namespace StudentTrackerApp.Controllers;

[Authorize(Roles = "Student")]
public class StudentController : Controller
{
    private readonly ApplicationDbContext _db;

    public StudentController(ApplicationDbContext db)
    {
        _db = db;
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Dashboard()
    {
        return View();
    }

    public async Task<IActionResult> Student()
    {
        //getting student id and passing it to viewData
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var student = await _db.Students
            .FirstOrDefaultAsync(s => s.UserId == userId);

        if (student == null)
        {
            return NotFound("Student not found");
        }

        ViewData["CurrentStudentId"] = student.Id;
        return View();
    }

    public async Task<IActionResult> AttendanceHistory()
    {
        //getting student id and passing it to viewData
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var student = await _db.Students
            .FirstOrDefaultAsync(s => s.UserId == userId);

        if (student == null)
        {
            return NotFound("Student not found");
        }

        ViewData["CurrentStudentId"] = student.Id;

        return View();
    }

    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

}