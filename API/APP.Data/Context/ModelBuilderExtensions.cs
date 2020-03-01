using APP.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace APP.Data.Context
{
    public static class ModelBuilderExtensions
    {
        public static void AddConfiguration<TEntity>(ModelBuilder modelBuilder, EntityTypeConfiguration<TEntity> configuration)
            where TEntity : BaseEntity
        {
            configuration.Map(modelBuilder.Entity<TEntity>());
        }
    }
}
