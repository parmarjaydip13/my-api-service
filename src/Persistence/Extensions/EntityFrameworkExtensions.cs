using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Primitives;

namespace Persistence.Extensions
{
    public static class EntityFrameworkExtensions
    {
        public static EntityTypeBuilder<T> Tap<T>(this EntityTypeBuilder<T> builder, Action<EntityTypeBuilder<T>> action) where T : Entity
        {
            action(builder);
            return builder;
        }
    }
}
