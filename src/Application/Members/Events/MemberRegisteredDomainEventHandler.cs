using Application.Abstractions;
using Application.Abstractions.Messaging;
using Domain.DomainEvents;
using Domain.Repositories;

namespace Application.Members.Events;
internal class MemberRegisteredDomainEventHandler : IDomainEventHandler<MemberRegisteredDomainEvent>
{
    private readonly IMemberRepository _memberRepository;
    private readonly IEmailService _emailService;

    public MemberRegisteredDomainEventHandler(IMemberRepository memberRepository, IEmailService emailService)
    {
        _memberRepository = memberRepository;
        _emailService = emailService;
    }

    public async Task Handle(MemberRegisteredDomainEvent notification, CancellationToken cancellationToken)
    {
        var member = await _memberRepository.GetByIdAsync(notification.MemberId, cancellationToken);

        if (member is null)
        {
            return;
        }
        await _emailService.SendWelcomeEmailAsync(member, cancellationToken);
    }
}
