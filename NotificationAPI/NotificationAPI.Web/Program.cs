using NotificationAPI.Application.Mappings;
using NotificationAPI.Application.Settings;
using NotificationAPI.Web.Extensions;
using NotificationAPI.Web.Middlewares;

var builder = WebApplication.CreateBuilder(args);
builder.Services.ConfigureSqlContext(builder.Configuration, "DefaultConnection");
builder.Services.ConfigureMassTransit(builder.Configuration, "MassTransitSettings");
builder.Services.ConfigureQuartz(builder.Configuration, "QuartzConfig");
builder.Services.ConfigureRepositories();
builder.Services.ConfigureServices();
builder.Services.AddEndpointsApiExplorer();

builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettingsConfig"));

builder.Services.AddAutoMapper(typeof(ApplicationMappingProfile));

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();

app.Run();