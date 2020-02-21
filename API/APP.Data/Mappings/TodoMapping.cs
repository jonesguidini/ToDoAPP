using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using APP.Domain.Entities;

namespace APP.Data.Mappings
{
    public class TodoMapping : IEntityTypeConfiguration<Todo>
    {
        public void Configure(EntityTypeBuilder<Todo> builder)
        {
            builder.ToTable("Todos");

            builder.HasKey(x => x.Id)
                .HasName("UserPK");

            //builder.Property(x => x.Created)
            //    .ValueGeneratedOnAdd()
            //    .HasColumnType("datetime2")
            //    .HasColumnName("Created");

            builder.Property(x => x.Title)
                .IsRequired(true)
                .HasColumnType("varchar(100)")
                .HasColumnName("Title");

        }
    }
}
