namespace NotificationAPI.Domain.Exceptions
{
    public class UserNotFoundException : NotFoundException
    {
        public UserNotFoundException(Guid id) 
            : base($"The user with the identifier {id} was not found.")
        {
        }
    }
}
