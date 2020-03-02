using APP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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

            builder.Property(x => x.IsDone)
                .IsRequired(false)
                .HasColumnType("bit")
                .HasColumnName("IsDone");

            builder
                .HasOne(x => x.DeletedByUser)
                .WithMany(y => y.ToDos)
                .HasForeignKey(x => x.DeletedByUserId)
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("ToDo.Possui.UserDeleted");
        }
    }
}
