using Domain.Primitives;

namespace Domain.DomainEvents;
public sealed record ProductCreateDomainEvent(Guid ProductId) : IDomainEvent
{

}
