
using System.ComponentModel.DataAnnotations;

namespace RaceDayMVC.Models
{
    public class RegisterModel
    {
        [Required]
        public string Username { get; set; } = "";

        [Required]
        public string Password { get; set; } = "";

        [Required]
        public string Role { get; set; } = "";

        // Participant fields
        public string? FullName { get; set; }
        public string? Email { get; set; }

        // Organiser fields
        public int OrganiserId { get; set; }
        public string? OrganiserName { get; set; }
    }
}
