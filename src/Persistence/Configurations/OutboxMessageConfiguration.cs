using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Constants;
using Persistence.Outbox;

namespace Persistence.Configurations;
internal sealed class OutboxMessageConfiguration : IEntityTypeConfiguration<OutBoxMessage>
{
    public void Configure(EntityTypeBuilder<OutBoxMessage> builder)
    {
        builder.ToTable(TableNames.OutboxMessages);
        builder.HasKey(x => x.Id);
        builder.Property(member => member.Id).ValueGeneratedNever();
    }
}
