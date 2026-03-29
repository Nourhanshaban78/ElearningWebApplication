namespace E_Learning.Service.DTOs.User
{
    public class CreateUserResponseDto
    {
        public Guid UserId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public string Role { get; set; } = string.Empty;
        public bool IsActive { get; set; } = false;
        public DateTime CreatedAt { get; set; }
        public string Password { get; set; } 
        public string Level { get; set; }


    }
}