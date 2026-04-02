using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.EntityFrameworkCore;
using TaskScheduler.Application.Interfaces;
using TaskScheduler.Infrastructure.Persistence;
using TaskScheduler.Infrastructure.Services;
using TaskScheduler.API.Jobs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(
        typeof(TaskScheduler.Application.Commands.SendWelcomeEmail
            .SendWelcomeEmailCommand).Assembly);

    cfg.RegisterServicesFromAssembly(
        typeof(TaskScheduler.Infrastructure.CleanOldOrdersHandler).Assembly);
});

builder.Services.AddHangfire(config =>
{
    config.UseSimpleAssemblyNameTypeSerializer()
       .UseRecommendedSerializerSettings()
       .UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddHangfireServer();

builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// http://localhost:5000/hangfire
app.UseHangfireDashboard("/hangfire");

using (var scope = app.Services.CreateScope())
{
    RecurringJobsSetup.RegisterRecurringJobs(scope.ServiceProvider);
}

app.MapControllers();

app.Run();
