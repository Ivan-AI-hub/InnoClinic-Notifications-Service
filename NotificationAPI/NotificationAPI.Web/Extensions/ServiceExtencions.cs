using MassTransit;
using Microsoft.EntityFrameworkCore;
using NotificationAPI.Application;
using NotificationAPI.Application.Abstraction;
using NotificationAPI.Domain.Interfaces;
using NotificationAPI.Persistence;
using NotificationAPI.Persistence.Repositories;
using NotificationAPI.Presentation.Consumers;
using NotificationAPI.Web.Settings;

namespace NotificationAPI.Web.Extensions
{
    public static class ServiceExtencions
    {
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration, string connectionStringSectionName)
        {
            var connection = configuration.GetConnectionString(connectionStringSectionName);
            services.AddDbContext<NotificationContext>(options =>
                                options.UseNpgsql(connection,
                                b => b.MigrationsAssembly(typeof(NotificationContext).Assembly.GetName().Name)));
        }
        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IPatientRepository, PatientRepository>();
        }
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IEmailSendingService, EmailSendingService>();
            services.AddScoped<IPatientService, PatientService>();
        }
        public static void ConfigureMassTransit(this IServiceCollection services, IConfiguration configuration, string massTransitSettingsName)
        {
            var settings = configuration.GetSection(massTransitSettingsName).Get<MassTransitSettings>();
            services.AddMassTransit(x =>
            {
                x.AddConsumersFromNamespaceContaining<PatientCreatedConsumer>();
                x.AddConsumeObserver<ConsumeObserver>();
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(settings.Host, settings.VirtualHost, h =>
                    {
                        h.Username(settings.UserName);
                        h.Password(settings.Password);
                    });
                    cfg.AddRawJsonSerializer();
                    cfg.ConfigureEndpoints(context);
                });
            });
        }
    }
}
