﻿// <auto-generated />
using System;
using APP.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace APP.Data.Migrations
{
    [DbContext(typeof(DBContext))]
    partial class DBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("APP.Domain.Entities.Todo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DateDeleted")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("DeletedByUserId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<ulong?>("IsDone")
                        .HasColumnName("IsDone")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnName("Title")
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id")
                        .HasName("UserPK");

                    b.HasIndex("DeletedByUserId");

                    b.ToTable("Todos");
                });

            modelBuilder.Entity("APP.Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Created")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("DateDeleted")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("DeletedByUserId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnName("Email")
                        .HasColumnType("varchar(100)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnName("PasswordHash")
                        .HasColumnType("varchar(1000)");

                    b.Property<string>("PasswordSalt")
                        .IsRequired()
                        .HasColumnName("PasswordSalt")
                        .HasColumnType("varchar(1000)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnName("Username")
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id")
                        .HasName("UserPK");

                    b.HasIndex("DeletedByUserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("APP.Domain.Entities.Todo", b =>
                {
                    b.HasOne("APP.Domain.Entities.User", "DeletedByUser")
                        .WithMany("ToDos")
                        .HasForeignKey("DeletedByUserId")
                        .HasConstraintName("ToDo.Possui.UserDeleted")
                        .OnDelete(DeleteBehavior.NoAction);
                });

            modelBuilder.Entity("APP.Domain.Entities.User", b =>
                {
                    b.HasOne("APP.Domain.Entities.User", "DeletedByUser")
                        .WithMany()
                        .HasForeignKey("DeletedByUserId")
                        .HasConstraintName("User.Possui.UserDeleted")
                        .OnDelete(DeleteBehavior.NoAction);
                });
#pragma warning restore 612, 618
        }
    }
}
