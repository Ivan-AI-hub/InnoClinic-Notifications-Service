namespace NotificationAPI.Domain.Exceptions
{
    public class NotificationNotFoundException : NotFoundException
    {
        public NotificationNotFoundException(Guid id)
            : base($"The notification with the identifier {id} was not found.")
        {
        }
    }
}
