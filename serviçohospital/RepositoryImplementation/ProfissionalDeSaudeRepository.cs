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

        public async Task<Consulta?> FinalizarConsulta(int consultaId, string prontuario)
        {
            var consulta = await _context.Consultas.FindAsync(consultaId);

            if (consulta != null)
            {
                consulta.Prontuario = prontuario;
                consulta.Status = StatusConsulta.Feita;

                await _context.SaveChangesAsync();
                return consulta;
            }

            return null; // retorno explícito se não encontrado
        }


        public async Task<Prescricao> EmitirPrescricao(int consultaId, string medicamento, string dosagem)
        {
            var prescrição = new Prescricao
            {
                ConsultaID = consultaId,
                Medicamento = medicamento,
                Dosagem = dosagem,
                DataPrescricao = DateTime.Now
            };
              await _context.Prescricoes.AddAsync(prescrição);
             await _context.SaveChangesAsync();
            return prescrição;
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
