using Microsoft.EntityFrameworkCore;
using serviçohospital.Context;
using serviçohospital.Models;
using serviçohospital.Repository;

namespace serviçohospital.RepositoryImplementation
{
    public class AdmistradorRepository : IAdministradorRepository
    {
        private readonly AppDbContext _context;

        public AdmistradorRepository(AppDbContext context)
        {
            _context = context;
        }
        
        public async Task<bool> CancelarConsultaAsync(int consultaId)
        {
           var consulta = await _context.Consultas.FirstOrDefaultAsync(x => x.Id == consultaId);
             consulta.Status  = StatusConsulta.Cancelada;

            if(consulta == null) return false;
             await _context.SaveChangesAsync();
            return true;
           
        }

        public async Task<ProfissionalSaude> CriarProfissionalAsync(ProfissionalSaude profissionalSaude)
        {
             _context.Profissionais.Add(profissionalSaude);
            await _context.SaveChangesAsync();
            return profissionalSaude;

        }

        public Task<bool> DesativarPacienteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ProfissionalSaude> DesativarProfissionalAsync(int idh)
        {
          var Consulta =  _context.Profissionais.FirstOrDefaultAsync(x => x.Id == idh);
            if (Consulta == null) return null;
            _context.Remove(Consulta);
            _context.SaveChangesAsync();
            return Consulta;
        }

       
          public async Task<Paciente> EditarPacienteAsync(Paciente paciente)
        {
            // 1. Buscar o paciente no banco de dados pelo ID
            var pacienteExistente = await _context.Pacientes.FirstOrDefaultAsync(p => p.Id == paciente.Id);

            // 2. Verificar se o paciente foi encontrado
            if (pacienteExistente == null)
                return null; // ou lançar uma exceção, dependendo da necessidade

            // 3. Atualizar os dados do paciente
            pacienteExistente.Nome = paciente.Nome;
            pacienteExistente.CPF = paciente.CPF;
            pacienteExistente.Telefone = paciente.Telefone;
            // Adicione qualquer outra propriedade que precise ser atualizada

            // 4. Salvar as alterações no banco de dados
            await _context.SaveChangesAsync();

            // Retornar o paciente atualizado (ou poderia retornar o pacienteExistente)
            return pacienteExistente;
          }
 
        

        public async Task<ProfissionalSaude> EditarProfissionalAsync(ProfissionalSaude profissional)
        {
            // 1. Buscar o paciente no banco de dados pelo ID
            var consulta = _context.Profissionais.FirstOrDefault(x => x.Id == profissional.Id);

            // 2. Verificar se o paciente foi encontrado
            if (profissional == null)
                return null; // ou lançar uma exceção, dependendo da necessidade

            // 3. Atualizar os dados do paciente
            profissional.Nome = profissional.Nome;
            profissional.Especialidade = profissional.Especialidade;
            
            
            await _context.SaveChangesAsync();
            
            // 4. Salvar as alterações no banco de dados

            // Retornar o paciente atualizado (ou poderia retornar o pacienteExistente)
            return profissional;
        }

        public Task<IEnumerable<Consulta>> ListarConsultasAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Paciente>> ListarPacientesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProfissionalSaude>> ListarProfissionaisAsync()
        {
            throw new NotImplementedException();
        }

        Task<bool> IAdministradorRepository.DesativarProfissionalAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
