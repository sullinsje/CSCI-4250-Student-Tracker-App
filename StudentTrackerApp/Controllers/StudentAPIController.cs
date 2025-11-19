using Microsoft.AspNetCore.Mvc;
using StudentTrackerApp.Services;
using StudentTrackerApp.Models.Entities;
using Microsoft.AspNetCore.Authorization;

namespace StudentTracker.Controllers;


[Route("api/[controller]")]
[ApiController]
[Authorize] 
public class StudentAPIController : ControllerBase
{
    private readonly IStudentRepository _studentRepo;

    public StudentAPIController(IStudentRepository studentRepo)
    {
        _studentRepo = studentRepo;
    }

    //gets all students with their attendance data
    [HttpGet("all")]
    public async Task<IActionResult> Get()
    {
        return Ok(await _studentRepo.ReadAllAsync());
    }

    //creates a student from passed in student object
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

    //updated endpoint for readability
    //gets single student (includes attendance data)
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

    // updates the POSTed student
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

    // deletes single student
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
    
    // add an attendance record to a student (ViewModel contains id to access the student)
    [HttpPost("attendanceRecord/add")]
    public async Task<IActionResult> AddAttendanceRecord([FromForm] AttendanceVM model)
    {
        //make attendance record from VM and create it with the Create function
        var attendanceRecord = new AttendanceRecord
        {
            Id = model.Id,
            StudentId = model.StudentId,
            Date = model.Date,
            ClockInLatitude = model.ClockInLatitude,
            ClockInLongitude = model.ClockInLongitude,
            ClockType = model.ClockType
        };

        await _studentRepo.CreateAttendanceRecordAsync(model.StudentId, attendanceRecord);

        //make a DTO to return as JSON
        var attendanceRecordDto = new
        {
            id = model.Id,
            studentId = model.StudentId,
            date = model.Date,
            clockInLatitude = model.ClockInLatitude,
            clockInLongitude = model.ClockInLongitude,
            clockType = model.ClockType
        };

        return Ok(attendanceRecordDto);
    }    
    
}

//view model to ensure received attendance record is formatted correctly
public class AttendanceVM
{
    public int Id { get; set; }
    public int StudentId { get; set; } 
    public DateOnly Date { get; set; }
    public double ClockInLatitude { get; set; }
    public double ClockInLongitude { get; set; }
    public bool ClockType {get; set; }
}