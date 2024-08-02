using Domain.Primitives;

namespace Domain.DomainEvents;

public sealed record MemberRegisteredDomainEvent(Guid MemberId) : IDomainEvent;
