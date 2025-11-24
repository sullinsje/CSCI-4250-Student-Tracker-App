using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using StudentTrackerApp.Models;

namespace StudentTrackerApp.Controllers;

/// <summary>
/// Public site controller for common pages such as the index and privacy.
/// </summary>
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Landing page for the application.
    /// </summary>
    public IActionResult Index()
    {
        return View();
    }

    /// <summary>
    /// Privacy information page.
    /// </summary>
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
