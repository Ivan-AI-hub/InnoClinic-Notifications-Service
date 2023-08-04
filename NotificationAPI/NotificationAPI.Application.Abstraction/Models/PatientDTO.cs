namespace NotificationAPI.Application.Abstraction.Models
{
    public class PatientDTO
    {
        public Guid Id { get; set; }
        public string Email { get; set; }

        public PatientDTO(Guid id, string email)
        {
            Id = id;
            Email = email;
        }
    }
}
