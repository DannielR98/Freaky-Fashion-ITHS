using FreakyFashion.Infrastructure;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddControllers();
builder.Services.AddFreakyFashionInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseAuthorization();

app.MapControllers();
app.MapGet("/health", () => Results.Ok(new { status = "Healthy", service = "Freaky Fashion" })).WithName("Health");

app.Run();
