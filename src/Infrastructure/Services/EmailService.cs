using Application.Abstractions;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services;

internal sealed class EmailService : IEmailService
{
    private readonly ILogger<EmailService> _logger;

    public EmailService(ILogger<EmailService> logger)
    {
        _logger = logger;
    }

    public Task SendWelcomeEmailAsync(Member? member, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Mail Send to member @{member}", member);

        return Task.CompletedTask;
    }
}

