using Domain.Entities;

namespace Application.Abstractions;

public interface IEmailService
{
    Task SendWelcomeEmailAsync(Member? member, CancellationToken cancellationToken);
}