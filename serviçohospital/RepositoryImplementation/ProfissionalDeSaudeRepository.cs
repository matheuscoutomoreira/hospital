using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using serviçohospital.Context;
using serviçohospital.Models;
using serviçohospital.Repository;

namespace serviçohospital.RepositoryImplementation
{
    public class ProfissionalDeSaudeRepository : IProfissionalDesaudeRepository
    {

        private readonly AppDbContext _context;

        public ProfissionalDeSaudeRepository(AppDbContext context)
        {
            _context = context;
        }

      

        public async Task<ProfissionalSaude> Createasync(ProfissionalSaude profissional)
        {
           _context.Profissionais.AddAsync(profissional);
            await _context.SaveChangesAsync();
            return profissional;
        }

        public async Task<ProfissionalSaude> UpdateAsync(ProfissionalSaude profissional)
        {
            _context.Profissionais.Update(profissional);
            await _context.SaveChangesAsync();
            return profissional;
        }

        public async Task<ProfissionalSaude> DeleteAsync(int id)
        {
           var profissional = _context.Profissionais.Find(id);
            if (profissional == null) return null;
             _context.Profissionais.Remove(profissional);
            await _context.SaveChangesAsync();
            return profissional;
        }

       
        

        public async Task<bool> ExisteregistroAsync(string registro)
        {
            return await _context.Profissionais.AnyAsync( x => x.CRM == registro);
           
            
        }

        public async Task<IEnumerable<Paciente>> GetPacientesAllAsync()
        {
            return await _context.Pacientes.ToListAsync();
        }

        public async Task<ProfissionalSaude> GetByIdAsync(int id)
        {
            var s = await _context.Profissionais.FindAsync(id);
            if (s == null) return null;
            return s;
        }

        public Task<List<Consulta>> ObterAgenda(int profissionalId)
        {
            return  _context.Consultas
                .Where(c => c.ProfissionalSaudeId == profissionalId && c.Status == StatusConsulta.Agendado)
                .OrderBy(c => c.DataHora)
                .ToListAsync();
        }

        public void FinalizarConsulta(int consultaId, string prontuario)
        {
            var consulta = _context.Consultas.Find(consultaId);

            if (consulta != null) {
                consulta.Prontuario = prontuario;
                consulta.Status = StatusConsulta.Feita;
                _context.SaveChangesAsync();
            }
        }

        public void EmitirPrescricao(int consultaId, string medicamento, string dosagem)
        {
            var prescrição = new Prescricao
            {
                ConsultaID = consultaId,
                Medicamento = medicamento,
                Dosagem = dosagem,
                DataPrescricao = DateTime.Now
            };
            _context.Prescricoes.Add(prescrição);
            _context.SaveChangesAsync();
        }

        public Task<List<Consulta>> ObterHistoricoPaciente(int pacienteId)
        {
            return _context.Consultas
                .Where(c => c.PacienteId == pacienteId && c.Status == StatusConsulta.Feita)
                .Include(c => c.Profissional)
                .Include( c => c.Prescricoes)
                .OrderByDescending(c => c.Status)
                .ToListAsync();
        }

      
    }
}
