using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using serviçohospital.Context;
using serviçohospital.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace serviçohospital.Repository
{
    public class PacientesRepository : IPacientesRepository
    {
        private readonly AppDbContext _context;

        public PacientesRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Paciente> CreateAsync(Paciente paciente)
        {
            _context.Pacientes.Add(paciente);
            await _context.SaveChangesAsync();
            return paciente;
        }

        public async Task<Paciente> UpdateAsync(Paciente paciente)
        {
            _context.Pacientes.Update(paciente);
            await _context.SaveChangesAsync();
            return paciente;
        }

        public async Task<Paciente> DeleteAsync(int id)
        {
            var paciente = await _context.Pacientes.FindAsync(id);
            if (paciente == null) return null;

            _context.Pacientes.Remove(paciente);
            await _context.SaveChangesAsync();
            return paciente;
        }

        public async Task<Consulta> CancelarConsultaAsync(int consultaId)
        {
            var consulta = await _context.Consultas.FirstOrDefaultAsync(x => x.Id == consultaId);
            consulta.Status = StatusConsulta.Cancelada;

            if (consulta == null) return null;
            await _context.SaveChangesAsync();
            return consulta;

        }

        public async Task<IEnumerable<Paciente>> GetAllAsync()
        {
            return await _context.Pacientes.ToListAsync();
        }

        public async Task<Paciente> GetByIdAsync(int id)
        {
            return await _context.Pacientes.FindAsync(id);
        }

        public async Task<Paciente> BuscarPorCpfAsync(string cpf)
        {
            return await _context.Pacientes.FirstOrDefaultAsync(p => p.CPF == cpf);
        }

        public async Task<Paciente> BuscarPorNomeAsync(string nome)
        {if (nome is null) throw new ArgumentNullException(nameof(nome));
            return await _context.Pacientes.FirstOrDefaultAsync(x => x.Nome == nome);
              
        }

        public async Task<Consulta> CriarConsultaAsync(Consulta consulta)
        {
            _context.Consultas.Add(consulta);
            await _context.SaveChangesAsync();
            return consulta;
        }

        public async Task<Historico> GetHistoricoAsync(int pacienteId)
        {
            return await _context.Historicos
                .Include(h => h.Consultas)
                    .ThenInclude(c => c.Prescricoes)
                .FirstOrDefaultAsync(h => h.PacienteId == pacienteId);
        }
    }
}
