using serviçohospital.Models;

namespace serviçohospital.Repository
{
    public interface IAdministradorRepository
    {
        // Gestão de Profissionais de Saúde

        Task<ProfissionalSaude> CriarProfissionalAsync(ProfissionalSaude profissionalSaude);
        Task<ProfissionalSaude> EditarProfissionalAsync(ProfissionalSaude profissional);
        Task<ProfissionalSaude> DesativarProfissionalAsync(int id);
        Task<IEnumerable<ProfissionalSaude>> ListarProfissionaisAsync();

        // Gestão de Pacientes

        Task<Paciente> EditarPacienteAsync(Paciente paciente);
        Task<ProfissionalSaude> DesativarPacienteAsync(int id);
        Task<IEnumerable<Paciente>> ListarPacientesAsync();

        // Gestão de Consultas

        Task<IEnumerable<Consulta>> ListarConsultasAsync();
        Task<bool> CancelarConsultaAsync(int consultaId);
    }
}
