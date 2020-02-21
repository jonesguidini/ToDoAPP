using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using APP.Domain.Entities;

namespace APP.Data.Mappings
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(x => x.Id)
                .HasName("UserPK");

            builder.Property(x => x.Created)
                .ValueGeneratedOnAdd()
                .HasColumnType("datetime2")
                .HasColumnName("Created");

            builder.Property(x => x.Username)
                .IsRequired(true)
                .HasColumnType("varchar(100)")
                .HasColumnName("Username");

            builder.Property(x => x.PasswordHash)
                .IsRequired(true)
                .HasColumnType("nvarchar(max)")
                .HasColumnName("PasswordHash");

            builder.Property(x => x.PasswordSalt)
                .IsRequired(true)
                .HasColumnType("nvarchar(max)")
                .HasColumnName("PasswordSalt");

            builder.Property(x => x.Email)
                .IsRequired(true)
                .HasColumnType("varchar(100)")
                .HasColumnName("Email");

            builder
                .HasOne(x => x.DeletedByUser)
                .WithMany()
                .HasForeignKey(x => x.DeletedByUserId)
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("User.Possui.UserDeleted");

        }
    }
}
