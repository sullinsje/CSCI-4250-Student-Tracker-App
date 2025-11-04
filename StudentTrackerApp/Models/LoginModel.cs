using System.ComponentModel.DataAnnotations;

namespace StudentTrackerAPI.Models 
{
    public class LoginModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = String.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = String.Empty;
        
    }
}