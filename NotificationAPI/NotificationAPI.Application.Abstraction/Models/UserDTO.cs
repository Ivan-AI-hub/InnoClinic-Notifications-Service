namespace NotificationAPI.Application.Abstraction.Models
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string Email { get; set; }

        public UserDTO(Guid id, string email)
        {
            Id = id;
            Email = email;
        }
    }
}
