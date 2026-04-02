using MediatR;
using TaskScheduler.Application.Interfaces;

namespace TaskScheduler.Application.Commands.SendWelcomeEmail;

public class SendWelcomeEmailHandler 
    : IRequestHandler<SendWelcomeEmailCommand, Unit>
{
    private readonly IEmailService _emailService;

    public SendWelcomeEmailHandler(IEmailService emailService)
        => _emailService = emailService;

    public async Task<Unit> Handle(
        SendWelcomeEmailCommand request,
        CancellationToken cancellationToken)
    {
        await _emailService.SendWelcomeEmailAsync(
            request.UserEmail,
            request.UserName);

        return Unit.Value;  
    }
}
