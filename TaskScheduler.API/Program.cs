using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.EntityFrameworkCore;
using TaskScheduler.Application.Interfaces;
using TaskScheduler.Infrastructure.Persistence;
using TaskScheduler.Infrastructure.Services;
using TaskScheduler.API.Jobs;

var builder = WebApplication.CreateBuilder(args);

// ── 1. الـ Database ──────────────────────────────────────────
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("TaskSchedulerDb"));

// ── 2. الـ MediatR ───────────────────────────────────────────
builder.Services.AddMediatR(cfg =>
{
    // Scan Application assembly
    cfg.RegisterServicesFromAssembly(
        typeof(TaskScheduler.Application.Commands.SendWelcomeEmail
            .SendWelcomeEmailCommand).Assembly);
    // Scan Infrastructure assembly (contains CleanOldOrdersHandler)
    cfg.RegisterServicesFromAssembly(
        typeof(TaskScheduler.Infrastructure.CleanOldOrdersHandler).Assembly);
});

// ── 3. الـ Hangfire ──────────────────────────────────────────
builder.Services.AddHangfire(config =>
    config.UseMemoryStorage());

builder.Services.AddHangfireServer();
// ده بيشغل الـ Hangfire Worker اللي بينفذ الـ Jobs

// ── 4. الـ Services ──────────────────────────────────────────
builder.Services.AddScoped<IEmailService, EmailService>();
// الـ Application Layer بتطلب IEmailService، والـ DI بيديها EmailService

// ── 5. الـ Controllers ───────────────────────────────────────
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ── 6. الـ Middleware ────────────────────────────────────────
app.UseSwagger();
app.UseSwaggerUI();

// ── 7. الـ Hangfire Dashboard ────────────────────────────────
// بتقدري تشوفي كل الـ Jobs اللي اتنفذت وفشلت على:
// http://localhost:5000/hangfire
app.UseHangfireDashboard("/hangfire");

// ── 8. الـ Recurring Jobs ────────────────────────────────────
using (var scope = app.Services.CreateScope())
{
    RecurringJobsSetup.RegisterRecurringJobs(scope.ServiceProvider);
}

app.MapControllers();

app.Run();
