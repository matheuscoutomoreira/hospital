using serviçohospital.Models;

namespace serviçohospital.Repository
{
    public interface IAdministradorRepository
    {
        // Gestão de Profissionais de Saúde

        Task<ProfissionalSaude> CriarProfissionalAsync(ProfissionalSaude profissionalSaude);
        Task<ProfissionalSaude> EditarProfissionalAsync(ProfissionalSaude profissional);
        Task<ProfissionalSaude> DesativarProfissionalAsync(int id);
        Task<Consulta> GetByIdAsync(int id);
        Task<IEnumerable<ProfissionalSaude>> ListarProfissionaisAsync();

        Task<ProfissionalSaude> GetProfissional(int id);

        // Gestão de Pacientes

        Task<Paciente> EditarPacienteAsync(Paciente paciente);
        Task<Paciente> DesativarPacienteAsync(int id);
        Task<IEnumerable<Paciente>> ListarPacientesAsync();

        // Gestão de Consultas

        Task<IEnumerable<Consulta>> ListarConsultasAsync();
        Task<Consulta> CancelarConsultaAsync(int consultaId);
    }
}
