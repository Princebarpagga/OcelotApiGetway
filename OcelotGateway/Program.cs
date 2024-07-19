using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using JwtAuthenticationManger;
using Ocelot.Cache.CacheManager;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Set up Serilog
//Log.Logger = new LoggerConfiguration()
//    .MinimumLevel.Debug()
//    .WriteTo.Console()
//    .WriteTo.File("logs/ocelot.txt", rollingInterval: RollingInterval.Day)
//    .CreateLogger();
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration) // Read from appsettings.json
    .CreateLogger();

builder.Host.UseSerilog();
builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();

// Load Ocelot configuration from JSON file
//builder.Configuration.AddJsonFile("ocelot.json");
//builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

// Add Ocelot and CacheManager services
builder.Services.AddOcelot(builder.Configuration)
    .AddCacheManager(settings => settings.WithDictionaryHandle());
//builder.Services.AddOcelot(builder.Configuration);
builder.Services.AddCustomJwtAuthentication();
builder.Services.AddControllers();
builder.Logging.AddConsole();

var app = builder.Build();

await app.UseOcelot();

app.UseAuthentication();
app.UseAuthorization();



// Handle requests
app.Run();
