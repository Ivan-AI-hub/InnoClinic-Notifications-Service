using MassTransit;
using NotificationAPI.Application.Abstraction;
using NotificationAPI.Application.Abstraction.Models;
using SharedEvents.Models;

namespace NotificationAPI.Presentation.Consumers
{
    public class PatientCreatedConsumer : IConsumer<PatientCreated>
    {
        private readonly IPatientService _patientService;

        public PatientCreatedConsumer(IPatientService patientService)
        {
            _patientService = patientService;
        }

        public async Task Consume(ConsumeContext<PatientCreated> context)
        {
            var message = context.Message;
            await _patientService.AddAsync(new PatientDTO(message.Id, message.Email));
        }
    }
}
