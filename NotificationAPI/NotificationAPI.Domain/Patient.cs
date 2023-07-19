namespace NotificationAPI.Domain
{
    public class Patient
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Email { get; private set; }

        private Patient() { }
        public Patient(string email)
        {
            Email = email;
        }
    }
}