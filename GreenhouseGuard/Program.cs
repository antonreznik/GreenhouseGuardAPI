using Application;
using GreenhouseGuard;
using GreenhouseGuard.Endpoints.Sensor;
using GreenhouseGuard.Hubs;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiServices();
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

var app = builder.Build();

app.UseExceptionHandler();
app.UseCors("Angular");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapSensorEndpoints();

app.MapHub<SensorHub>("/hubs/sensors");

app.Run();
