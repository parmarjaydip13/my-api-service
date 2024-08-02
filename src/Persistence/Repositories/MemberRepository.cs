using Domain.Entities;
using Domain.Repositories;

namespace Persistence.Repositories;
internal sealed class MemberRepository : Repository<Member>, IMemberRepository
{
    public MemberRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
    {

    }
}
