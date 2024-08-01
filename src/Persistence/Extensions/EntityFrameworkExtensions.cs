using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
