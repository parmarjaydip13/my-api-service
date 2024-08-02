using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Constants;
using Persistence.Extensions;

namespace Persistence.Configurations;
internal class MemberConfiguration : IEntityTypeConfiguration<Member>
{
    public void Configure(EntityTypeBuilder<Member> builder) => builder.Tap(ConfigureDataStructure);

    private static void ConfigureDataStructure(EntityTypeBuilder<Member> builder)
    {
        builder.ToTable(TableNames.Members);
        builder.HasKey(member => member.Id);
        builder.Property(member => member.Id).ValueGeneratedNever();
        builder.Property(member => member.FirstName).HasMaxLength(20).IsRequired();
        builder.Property(member => member.Lastname).HasMaxLength(20).IsRequired();
        builder.Property(member => member.Email).HasMaxLength(50).IsRequired();
    }
}
