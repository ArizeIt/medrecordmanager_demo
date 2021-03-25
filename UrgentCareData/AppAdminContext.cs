using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrgentCareData;
using UrgentCareData.Models;

namespace MedRecordManager.Data
{
    public partial class AppAdminContext : DbContext
    {
        public AppAdminContext(string connectionString) : base(GetOptions(connectionString))
        {

        }

        public AppAdminContext(DbContextOptions<AppAdminContext> options) : base(options)
        {

        }

        public virtual DbSet<CompanyProfile> CompanyProfile { get; set; }
        public virtual DbSet<UserClinic> UserClinic { get; set; }
        public virtual DbSet<UserCompany> UserCompany { get; set; }
        public virtual DbSet<UserOfficeKey> UserOfficeKey { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=localhost; Database=urgentcare; Trusted_Connection=True;MultipleActiveResultSets=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CompanyProfile>(entity =>
            {
                entity.ToTable("CompanyProfile", "AppAdmin");

                entity.Property(e => e.Address1)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Address2)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DbConnection).HasMaxLength(500);

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Fax)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.WebApiUri).HasMaxLength(500);

                entity.Property(e => e.Zip)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserClinic>(entity =>
            {
                entity.ToTable("UserClinic", "AppAdmin");

                entity.HasIndex(e => new { e.UserId, e.ClinicId })
                    .HasName("uq_UserClinic")
                    .IsUnique();

                entity.Property(e => e.ClinicId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UserId).IsRequired();
            });

            modelBuilder.Entity<UserCompany>(entity =>
            {
                entity.ToTable("UserCompany", "AppAdmin");

                entity.HasIndex(e => new { e.UserId, e.CompanyId })
                    .HasName("uq_UserCompany")
                    .IsUnique();

                entity.Property(e => e.UserId).IsRequired();

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.UserCompany)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserCompany_CompanyProfile");
            });

            modelBuilder.Entity<UserOfficeKey>(entity =>
            {
                entity.ToTable("UserOfficeKey", "AppAdmin");

                entity.HasIndex(e => new { e.UserId, e.OfficeKey })
                    .HasName("uq_UserOfficeKey")
                    .IsUnique();

                entity.Property(e => e.UserId).IsRequired();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        private static DbContextOptions GetOptions(string connectionString)
        {
            return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), connectionString).Options;
        }
    }
}