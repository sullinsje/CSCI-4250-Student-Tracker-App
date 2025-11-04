using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using StudentTrackerApp.Services;
using StudentTrackerApp.Models.Entities;
using Microsoft.AspNetCore.Authorization;
namespace StudentTracker.Controllers;


[Route("api/[controller]")]
[ApiController]
// [Authorize]
public class StudentAPIController : ControllerBase
{
    private readonly IStudentRepository _studentRepo;

    public StudentAPIController(IStudentRepository studentRepo)
    {
        _studentRepo = studentRepo;
    }

    [HttpGet("all")]
    public async Task<IActionResult> Get()
    {
        return Ok(await _studentRepo.ReadAllAsync());
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateStudent([FromForm] Student newStudent)
    {
        await _studentRepo.CreateAsync(newStudent);
        return CreatedAtAction("Get", new { id = newStudent.id }, newStudent);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var student = await _studentRepo.ReadAsync(id);
        if (student == null)
        {
            return NotFound();
        }
        return Ok(student);
    }

    [HttpPut("update")]
    public async Task<IActionResult> Put([FromForm] Student student)
    {
        await _studentRepo.UpdateAsync(student.id, student);
        return NoContent();
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _studentRepo.DeleteAsync(id);
        return NoContent(); // 204 as per HTTP specification
    }

}