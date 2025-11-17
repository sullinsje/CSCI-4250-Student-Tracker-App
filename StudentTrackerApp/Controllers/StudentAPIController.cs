using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using StudentTrackerApp.Services;
using StudentTrackerApp.Models.Entities;
using Microsoft.AspNetCore.Authorization;

namespace StudentTracker.Controllers;


[Route("api/[controller]")]
[ApiController]
[Authorize] // Uncomment this once attendanceRecordization is fully implemented for API endpoints
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

    [HttpGet("one/{id}")]
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
    
    [HttpPost("attendanceRecord/add")]
    public async Task<IActionResult> AddAttendanceRecord([FromForm] AttendanceVM model)
    {
        var attendanceRecord = new AttendanceRecord
        {
            Id = model.Id,
            StudentId = model.StudentId,
            Date = model.Date,
            ClockInLatitude = model.ClockInLatitude,
            ClockInLongitude = model.ClockInLongitude
        };

        await _studentRepo.CreateAttendanceRecordAsync(model.StudentId, attendanceRecord);

        var attendanceRecordDto = new
        {
            id = model.Id,
            studentId = model.StudentId,
            date = model.Date,
            clockInLatitude = model.ClockInLatitude,
            clockInLongitude = model.ClockInLongitude
        };

        return Ok(attendanceRecordDto);
    }    
    
}

public class AttendanceVM
{
    public int Id { get; set; }
    public int StudentId { get; set; } 
    public DateOnly Date { get; set; }
    public double ClockInLatitude { get; set; }
    public double ClockInLongitude { get; set; }
}