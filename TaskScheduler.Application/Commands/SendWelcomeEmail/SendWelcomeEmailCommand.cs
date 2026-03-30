using MediatR;

namespace TaskScheduler.Application.Commands.SendWelcomeEmail;

public class SendWelcomeEmailCommand : IRequest<Unit>
{
    public string UserEmail { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
}
