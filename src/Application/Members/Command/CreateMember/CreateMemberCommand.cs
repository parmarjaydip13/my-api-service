using Application.Abstractions.Messaging;

namespace Application.Members.Command.CreateMember;

public sealed record CreateMemberCommand(string FirstName, string LastName, string Email)
    : ICommand<Guid>, ICommand;
