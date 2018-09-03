using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DemoElasticApplication.EntityFramework.Models
{
    public partial class CLHContext : DbContext
    {
        public CLHContext()
        {
        }

        public CLHContext(DbContextOptions<CLHContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AgendaContatos2> AgendaContatos2 { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=CQI-DEV-2252\\SQLEXPRESS;Initial Catalog=clh;User ID=dev;Password=123;Trusted_Connection=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AgendaContatos2>(entity =>
            {
                entity.ToTable("agendaContatos2");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Celular).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Email).HasMaxLength(150);

                entity.Property(e => e.IdEspecialidade).HasColumnName("idEspecialidade");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(150);
            });
        }
    }
}
