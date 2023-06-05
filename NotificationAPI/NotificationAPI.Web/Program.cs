using NotificationAPI.Application.Mappings;
using NotificationAPI.Web.Extensions;
using NotificationAPI.Web.Middlewares;

var builder = WebApplication.CreateBuilder(args);
builder.Services.ConfigureSqlContext(builder.Configuration, "DefaultConnection");
builder.Services.ConfigureMassTransit(builder.Configuration, "MassTransitSettings");
builder.Services.ConfigureRepositories();
builder.Services.ConfigureServices();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAutoMapper(typeof(ApplicationMappingProfile));
var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();

app.Run();