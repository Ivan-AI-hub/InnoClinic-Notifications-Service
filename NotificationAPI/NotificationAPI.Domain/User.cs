namespace NotificationAPI.Domain
{
    public class User
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Email { get; private set; }

        public User(string email)
        {
            Email = email;
        }
    }
}