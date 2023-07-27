using NotificationAPI.Application.Mappings;
using NotificationAPI.Application.Settings;
using NotificationAPI.Persistence;
using NotificationAPI.Presentation.Controllers;
using NotificationAPI.Web.Extensions;
using NotificationAPI.Web.Middlewares;

var builder = WebApplication.CreateBuilder(args);
builder.Services.ConfigureLogger(builder.Configuration, builder.Environment, "ElasticConfiguration:Uri");

builder.Services.ConfigureSqlContext(builder.Configuration, "DefaultConnection");
builder.Services.ConfigureMassTransit(builder.Configuration, "MassTransitSettings");
//builder.Services.ConfigureQuartz(builder.Configuration, "QuartzConfig");
builder.Services.ConfigureRepositories();
builder.Services.ConfigureServices();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers().AddApplicationPart(typeof(NotificationController).Assembly);

builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettingsConfig"));

builder.Services.AddAutoMapper(typeof(ApplicationMappingProfile));

var app = builder.Build();

app.MigrateDatabase<NotificationContext>();

app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();

app.MapControllers();

app.Run();