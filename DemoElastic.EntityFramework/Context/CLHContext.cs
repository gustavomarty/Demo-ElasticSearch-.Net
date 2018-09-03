using DemoElastic.Model.Agenda;
using Microsoft.EntityFrameworkCore;

namespace DemoElastic.EntityFramework
{
    public partial class CLHContext : DbContext
    {
        public CLHContext(DbContextOptions<CLHContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AgendaContatos2> AgendaContatos2 { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
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
