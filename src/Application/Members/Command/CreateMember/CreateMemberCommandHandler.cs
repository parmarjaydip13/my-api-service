using Application.Abstractions.Messaging;
using Domain.Entities;
using Domain.Repositories;
using SharedKernel;

namespace Application.Members.Command.CreateMember;
internal sealed class CreateMemberCommandHandler : ICommandHandler<CreateMemberCommand, Guid>
{
    private readonly IMemberRepository _memberRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateMemberCommandHandler(IMemberRepository memberRepository, IUnitOfWork unitOfWork)
    {
        _memberRepository = memberRepository;
        this._unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
    {
        var member = Member.Create(request.FirstName, request.LastName, request.Email);

        _memberRepository.Add(member);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return member.Id;
    }
}
