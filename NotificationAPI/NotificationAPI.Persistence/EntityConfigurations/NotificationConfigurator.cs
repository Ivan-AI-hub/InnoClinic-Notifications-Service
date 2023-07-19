using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NotificationAPI.Domain;

namespace NotificationAPI.Persistence.EntityConfigurations
{
    internal class NotificationConfigurator : IEntityTypeConfiguration<ScheduledNotification>
    {
        public void Configure(EntityTypeBuilder<ScheduledNotification> builder)
        {
        }
    }
}
