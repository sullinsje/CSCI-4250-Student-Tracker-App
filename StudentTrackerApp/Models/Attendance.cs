namespace StudentTrackerApp.Models
{
    public class Attendance
    {
        public int Id { get; set; }
        public string StudentId { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public TimeSpan ClockInTime { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        
        // Helper property to display location as a formatted string
        public string Location => $"{Latitude:F6}, {Longitude:F6}";
    }
}
