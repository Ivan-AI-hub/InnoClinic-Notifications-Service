using MassTransit;
using Microsoft.EntityFrameworkCore;
using NotificationAPI.Application;
using NotificationAPI.Application.Abstraction;
using NotificationAPI.Application.Jobs;
using NotificationAPI.Domain.Interfaces;
using NotificationAPI.Persistence;
using NotificationAPI.Persistence.Repositories;
using NotificationAPI.Presentation.Consumers;
using NotificationAPI.Web.Settings;
using Quartz;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using System.Reflection;

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
            services.AddScoped<IScheduledNotificationRepository, NotificationRepository>();
        }
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IEmailSendingService, EmailSendingService>();
            services.AddScoped<IPatientService, PatientService>();
        }
        
        public static void ConfigureLogger(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment, string elasticUriSection)
        {
            services.AddSerilog((context, loggerConfiguration) =>
            {
                loggerConfiguration.Enrich.FromLogContext()
                    .Enrich.WithMachineName()
                    .WriteTo.Console()
                    .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(configuration[elasticUriSection]))
                    {
                        IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name!.ToLower().Replace(".", "-")}-{environment.EnvironmentName?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}",
                        AutoRegisterTemplate = true,
                        NumberOfShards = 2,
                        NumberOfReplicas = 1
                    })
                    .Enrich.WithProperty("Environment", environment.EnvironmentName)
                    .ReadFrom.Configuration(configuration);
            });
        }
        public static void ConfigureQuartz(this IServiceCollection services, IConfiguration configuration, string quartzSettingsSectionName)
        {
            var quartzSettings = configuration.GetSection(quartzSettingsSectionName).Get<QuartzSettings>();
            services.AddQuartz(q =>
            {
                q.UseMicrosoftDependencyInjectionJobFactory();
                q.AddJobAndTrigger<SendNotificationJob>(configuration, quartzSettings.SendNotificationCron);
            });
            services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
        }
        public static void ConfigureMassTransit(this IServiceCollection services, IConfiguration configuration, string massTransitSettingsSectionName)
        {
            var settings = configuration.GetSection(massTransitSettingsSectionName).Get<MassTransitSettings>();
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
                    cfg.ConfigureEndpoints(context, new KebabCaseEndpointNameFormatter(true));
                });
            });
        }
    }
}
