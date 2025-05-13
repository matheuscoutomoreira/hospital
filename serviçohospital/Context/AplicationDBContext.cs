using Microsoft.EntityFrameworkCore;
using serviçohospital.Models;

namespace serviçohospital.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<ProfissionalSaude> Profissionais { get; set; }
        public DbSet<Consulta> Consultas { get; set; }
        public DbSet<Prescricao> Prescricoes { get; set; }
        public DbSet<Administracao> Administracoes { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Seguranca> Segurancas { get; set; }
        public DbSet<Historico> Historicos { get; set; }

        // Método para configurar os relacionamentos
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relacionamento 1:1 entre Paciente e Historico
            modelBuilder.Entity<Paciente>()
                .HasOne(p => p.Historico)
                .WithOne(h => h.Paciente)
                .HasForeignKey<Historico>(h => h.PacienteId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
