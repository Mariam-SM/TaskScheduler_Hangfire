using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskScheduler.Application.Commands.SendWelcomeEmail;

namespace TaskScheduler.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IBackgroundJobClient _backgroundJobClient;

    public UsersController(
        IMediator mediator,
        IBackgroundJobClient backgroundJobClient)
    {
        _mediator = mediator;
        _backgroundJobClient = backgroundJobClient;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
    {
        
        _backgroundJobClient.Enqueue(
            () => _mediator.Send(
                new SendWelcomeEmailCommand
                {
                    UserEmail = dto.Email,
                    UserName = dto.Name
                },
                CancellationToken.None)
        );

        return Ok(new
        {
            Message = $"Welcome {dto.Name}! Registration successful. Check your email.",
            Email = dto.Email
        });
    }
}


