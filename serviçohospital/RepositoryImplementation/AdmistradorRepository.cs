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

        public async Task<Consulta> GetByIdAsync(int id)
        {
            var res = await _context.Consultas.FirstOrDefaultAsync(x => x.Id == id);
            return res;
        }

        // cabcela consultas
        public async Task<Consulta> CancelarConsultaAsync(int consultaId)
        {
            var consulta = await _context.Consultas.FirstOrDefaultAsync(x => x.Id == consultaId);
            if (consulta == null) return null;
            consulta.Status = StatusConsulta.Cancelada;


            await _context.SaveChangesAsync();
            return consulta;

        }

        // criarprofissional
        public async Task<ProfissionalSaude> CriarProfissionalAsync(ProfissionalSaude profissionalSaude)
        {
            _context.Profissionais.Add(profissionalSaude);
            await _context.SaveChangesAsync();
           
            return profissionalSaude;

        }


        //desativar pacintes
        public async Task<Paciente> DesativarPacienteAsync(int id)
        {
            var consulta =  await _context.Pacientes.FindAsync(id);
            if (consulta == null) return null;
            _context.Pacientes.Remove(consulta);
           await _context.SaveChangesAsync();
            return consulta;
        }

        // desativar profissional

        public async Task<ProfissionalSaude> DesativarProfissionalAsync(int idh)
        {
            var Consulta = await  _context.Profissionais.FirstOrDefaultAsync(x => x.Id == idh);
            if (Consulta == null) return null;
            _context.Profissionais.Remove(Consulta);
            await _context.SaveChangesAsync();
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

        public async Task<ProfissionalSaude> GetProfissional(int id)
        {
            var resposta = await _context.Profissionais.FirstOrDefaultAsync(x => x.Id == id);
            if (resposta == null) return null;
            return resposta;
        }

        public async Task<IEnumerable<Consulta>> ListarConsultasAsync()
        {
            return await _context.Consultas.ToListAsync();
        }

        public async Task<IEnumerable<Paciente>> ListarPacientesAsync()
        {

            return await _context.Pacientes.ToListAsync();

        }

        public async Task<IEnumerable<ProfissionalSaude>> ListarProfissionaisAsync()
        {

            return await _context.Profissionais.ToListAsync();

        }


    }
}
