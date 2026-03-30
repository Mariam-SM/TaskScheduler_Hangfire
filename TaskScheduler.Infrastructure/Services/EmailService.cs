using TaskScheduler.Application.Interfaces;

namespace TaskScheduler.Infrastructure.Services;

// ده الـ Implementation الحقيقية للـ IEmailService
// هنا بنعمل Simulation للإيميل عشان مش محتاجين SMTP حقيقي للتيست
public class EmailService : IEmailService
{
    public async Task SendWelcomeEmailAsync(string toEmail, string userName)
    {
        // في production هنا بنستخدم SendGrid أو SMTP حقيقي
        // دلوقتي بنعمل simulate بـ delay
        await Task.Delay(2000); // بيمثل إن الإيميل بياخد وقت

        Console.WriteLine($"[EmailService] Welcome email sent to {userName} at {toEmail}");
    }
}
