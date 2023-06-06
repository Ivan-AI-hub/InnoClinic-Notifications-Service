namespace NotificationAPI.Domain.Exceptions
{
    public class PatientNotFoundException : NotFoundException
    {
        public PatientNotFoundException(Guid id)
            : base($"The patient with the identifier {id} was not found.")
        {
        }
    }
}
