using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CSU_Core.Models
{
    public partial class ModelContext : DbContext
    {
        public ModelContext()
        {
        }

        public ModelContext(DbContextOptions<ModelContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Item> Items { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Supplydocument> Supplydocuments { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Warehouse> Warehouses { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseOracle("User Id=CSU;Password=12345678;Data Source=localhost:1521/xepdb1");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("CSU")
                .UseCollation("USING_NLS_COMP");

            modelBuilder.Entity<Item>(entity =>
            {
                entity.ToTable("ITEMS");

                entity.Property(e => e.Itemid)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ITEMID");

                entity.Property(e => e.Itemdescription)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("ITEMDESCRIPTION");

                entity.Property(e => e.Itemname)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("ITEMNAME");

                entity.Property(e => e.Quantity)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("QUANTITY");

                entity.Property(e => e.Warehouseid)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("WAREHOUSEID");

                entity.HasOne(d => d.Warehouse)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.Warehouseid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("SYS_C009957");
            });

           
            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("ROLE");

                entity.HasIndex(e => e.Name, "SYS_C009858")
                    .IsUnique();

                entity.Property(e => e.Roleid)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ROLEID");

                entity.Property(e => e.Name)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("NAME");
            });

            modelBuilder.Entity<Supplydocument>(entity =>
            {
                entity.HasKey(e => e.Documentid)
                    .HasName("SYS_C009961");

                entity.ToTable("SUPPLYDOCUMENTS");

                entity.Property(e => e.Documentid)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("DOCUMENTID");

                entity.Property(e => e.Createdby)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("CREATEDBY");

                entity.Property(e => e.Createddatetime)
                    .HasPrecision(6)
                    .HasColumnName("CREATEDDATETIME")
                    .HasDefaultValueSql("current_timestamp");

                entity.Property(e => e.Documentname)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("DOCUMENTNAME");

                entity.Property(e => e.Documentsubject)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("DOCUMENTSUBJECT");

                entity.Property(e => e.Itemid)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ITEMID");

                entity.Property(e => e.Status)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("STATUS");

                entity.Property(e => e.Warehouseid)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("WAREHOUSEID");

                entity.HasOne(d => d.CreatedbyNavigation)
                    .WithMany(p => p.Supplydocuments)
                    .HasForeignKey(d => d.Createdby)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("SYS_C009962");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.Supplydocuments)
                    .HasForeignKey(d => d.Itemid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("SYS_C009964");

                entity.HasOne(d => d.Warehouse)
                    .WithMany(p => p.Supplydocuments)
                    .HasForeignKey(d => d.Warehouseid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("SYS_C009963");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("USERS");

                entity.HasIndex(e => e.Username, "SYS_C009941")
                    .IsUnique();

                entity.HasIndex(e => e.Email, "SYS_C009942")
                    .IsUnique();

                entity.Property(e => e.Userid)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("USERID");

                entity.Property(e => e.Email)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Firstname)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("FIRSTNAME");

                entity.Property(e => e.Lastname)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("LASTNAME");

                entity.Property(e => e.Password)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("PASSWORD");

                entity.Property(e => e.Roleid)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ROLEID");

                entity.Property(e => e.Username)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("USERNAME");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.Roleid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("SYS_C009943");
            });

            modelBuilder.Entity<Warehouse>(entity =>
            {
                entity.ToTable("WAREHOUSES");

                entity.HasIndex(e => e.Warehousename, "SYS_C009951")
                    .IsUnique();

                entity.Property(e => e.Warehouseid)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("WAREHOUSEID");

                entity.Property(e => e.Createdby)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("CREATEDBY");

                entity.Property(e => e.Createddatetime)
                    .HasPrecision(6)
                    .HasColumnName("CREATEDDATETIME")
                    .HasDefaultValueSql("current_timestamp\n");

                entity.Property(e => e.Warehousedescription)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("WAREHOUSEDESCRIPTION");

                entity.Property(e => e.Warehousename)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("WAREHOUSENAME");

                entity.HasOne(d => d.CreatedbyNavigation)
                    .WithMany(p => p.Warehouses)
                    .HasForeignKey(d => d.Createdby)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("SYS_C009952");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
