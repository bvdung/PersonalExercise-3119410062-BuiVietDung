using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Pay1194
{
    public partial class PayDBContext : DbContext
    {
        public PayDBContext()
        {
        }

        public PayDBContext(DbContextOptions<PayDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<PaymentRecord> PaymentRecords { get; set; } = null!;
        public virtual DbSet<TaxYear> TaxYears { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-URUJDSO;Initial Catalog=PayDB;Trusted_Connection=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.Address).HasMaxLength(150);

                entity.Property(e => e.City).HasMaxLength(50);

                entity.Property(e => e.DOB).HasColumnName("DOB");

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.MiddleName).HasMaxLength(50);

                entity.Property(e => e.NationalInsuranceNo).HasMaxLength(50);

                entity.Property(e => e.PostCode).HasMaxLength(50);
            });

            modelBuilder.Entity<PaymentRecord>(entity =>
            {
                entity.Property(e => e.ContractualEarnings).HasColumnType("money");

                entity.Property(e => e.ContractualHours).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.FullName).HasMaxLength(100);

                entity.Property(e => e.HourlyRate).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.HoursWorked).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.NetPayment).HasColumnType("money");

                entity.Property(e => e.Nic)
                    .HasColumnType("money")
                    .HasColumnName("NIC");

                entity.Property(e => e.OvertimeEarnings).HasColumnType("money");

                entity.Property(e => e.OvertimeHours).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Slc)
                    .HasColumnType("money")
                    .HasColumnName("SLC");

                entity.Property(e => e.Tax).HasColumnType("money");

                entity.Property(e => e.TotalDeduction).HasColumnType("money");

                entity.Property(e => e.TotalEarnings).HasColumnType("money");

                entity.Property(e => e.UnionFee).HasColumnType("money");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.PaymentRecords)
                    .HasForeignKey(d => d.EmployeeId);

                entity.HasOne(d => d.TaxYear)
                    .WithMany(p => p.PaymentRecords)
                    .HasForeignKey(d => d.TaxYearId);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
