using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProjectManagement.BLL.Extensions;
using Serilog;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

// Configure Serilog for per-run logging
string logFileName = $"Logs/run_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File(logFileName)
    .CreateLogger();

builder.Services.AddSerilog();

builder.Services
    .AddApplicationInsightsTelemetryWorkerService()
    .ConfigureFunctionsApplicationInsights();

builder.Services.AddSharedServices(builder.Configuration);

builder.Build().Run();
