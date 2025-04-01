
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
        public DbSet<Administracao> administracaos { get; set; }
        public DbSet<Usuario> usuarios { get; set; }
        public DbSet<Seguranca> seguranca { get;set; }
       
       
    }
  
}
