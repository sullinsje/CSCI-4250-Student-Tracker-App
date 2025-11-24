using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentTrackerApp.Models;
using StudentTrackerApp.Services;

namespace StudentTrackerApp.Controllers;

/// <summary>
/// Controller that contains student-facing pages and actions such as
/// dashboard and viewing attendance history. Actions in this controller
/// assume the caller is authenticated as a student.
/// </summary>
[Authorize(Roles = "Student")]
public class StudentController : Controller
{
    private readonly ApplicationDbContext _db;

    /// <summary>
    /// Creates a new <see cref="StudentController"/> instance.
    /// </summary>
    /// <param name="db">The application's <see cref="ApplicationDbContext"/>.</param>
    public StudentController(ApplicationDbContext db)
    {
        _db = db;
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    /// <summary>
    /// Returns the student dashboard view.
    /// </summary>
    public IActionResult Dashboard()
    {
        return View();
    }

    /// <summary>
    /// Loads the current student's profile page. Resolves the student by
    /// matching the authenticated user's ID to the <see cref="Models.Entities.Student.UserId"/>.
    /// </summary>
    /// <returns>The student profile view or <see cref="NotFoundResult"/> when the student is absent.</returns>
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

    /// <summary>
    /// Presents the authenticated student's attendance history.
    /// </summary>
    /// <returns>The attendance history view or <see cref="NotFoundResult"/> if the student is not found.</returns>
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

    /// <summary>
    /// Returns the shared error view populated with the current request id.
    /// </summary>
    /// <returns>Error view.</returns>
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

}