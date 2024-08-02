using Domain.DomainEvents;
using Domain.Primitives;

namespace Domain.Entities;
public sealed class Member : AggregateRoot
{
    private Member(Guid id, string firstName, string lastname, string email) : base(id)
    {
        FirstName = firstName;
        Lastname = lastname;
        Email = email;
    }

    public string FirstName { get; set; }
    public string Lastname { get; }
    public string Email { get; set; }

    public static Member Create(string firstname, string lastname, string email)
    {
        var member = new Member(Guid.NewGuid(), firstname, lastname, email);
        
        member.RaiseDomainEvent(new MemberRegisteredDomainEvent(member.Id));
        
        return member;
    }

}
