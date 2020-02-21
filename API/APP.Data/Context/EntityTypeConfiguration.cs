using Microsoft.EntityFrameworkCore.Metadata.Builders;
using APP.Domain.Entities;

namespace APP.Data.Context
{
    public abstract class EntityTypeConfiguration<TEntity> where TEntity : BaseEntity
    {
        public abstract void Map(EntityTypeBuilder<TEntity> builder);
    }
}
