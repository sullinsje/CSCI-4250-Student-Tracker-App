using System.ComponentModel.DataAnnotations;

namespace StudentTrackerApp.Models 
{
    public class LoginModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = String.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = String.Empty;

        public bool RememberMe { get; set; } = false;
        
        [Required]
        public string RoleName { get; set; } = string.Empty;
        }
}