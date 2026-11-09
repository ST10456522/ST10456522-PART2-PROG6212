namespace RaceDayMVC.Models
{
    public class LoginModel
    {
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
    }

    public class LoginResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = "";
        public string Role { get; set; } = "";
        public int UserId { get; set; }
    }
}