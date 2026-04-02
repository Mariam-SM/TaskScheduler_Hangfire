using TaskScheduler.Application.Interfaces;

namespace TaskScheduler.Infrastructure.Services;


public class EmailService : IEmailService
{
    public async Task SendWelcomeEmailAsync(string toEmail, string userName)
    {
     
        await Task.Delay(2000); 

        Console.WriteLine($"[EmailService] Welcome email sent to {userName} at {toEmail}");
    }
}
