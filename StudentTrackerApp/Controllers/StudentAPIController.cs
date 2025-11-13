using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using StudentTrackerApp.Services;
using StudentTrackerApp.Models.Entities;
using Microsoft.AspNetCore.Authorization;

namespace StudentTracker.Controllers;


[Route("api/[controller]")]
[ApiController]
[Authorize] // Uncomment this once authorization is fully implemented for API endpoints
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
    public async Task<IActionResult> CreateStudent([FromBody] Student newStudent)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _studentRepo.CreateAsync(newStudent);
        
        return CreatedAtAction(nameof(Get), new { id = newStudent.Id }, newStudent);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var student = await _studentRepo.ReadAsync(id);
        if (student == null)
        {
            return NotFound($"Student with ID {id} not found.");
        }
        return Ok(student);
    }

    [HttpPut("update")]
    public async Task<IActionResult> Put([FromBody] Student student)
    {
        if (student.Id == 0)
        {
            return BadRequest("Student ID is required for update operation.");
        }

        await _studentRepo.UpdateAsync(student.Id, student);
        
        return NoContent();
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var student = await _studentRepo.ReadAsync(id);
        if (student == null)
        {
            return NotFound($"Student with ID {id} not found for deletion."); 
        }

        await _studentRepo.DeleteAsync(id);
        
        return NoContent(); 
    }
}