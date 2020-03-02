using APP.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APP.Data.Context
{
    public abstract class EntityTypeConfiguration<TEntity> where TEntity : BaseEntity
    {
        public abstract void Map(EntityTypeBuilder<TEntity> builder);
    }
}
