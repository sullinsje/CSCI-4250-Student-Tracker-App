namespace StudentTrackerApp.Models.Entities;

public class Student
{
    public int id { get; set; }
    public string name { get; set; } = String.Empty;
    public double latitude { get; set; }
    public double longitude { get; set; }
}
