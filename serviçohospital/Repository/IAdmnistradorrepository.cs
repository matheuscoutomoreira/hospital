using serviçohospital.Models;

namespace serviçohospital.Repository
{
    public interface IAdmnistradorrepository
    {
        // gestão de profissionais

        Task<ProfissionalSaude> CriarProfissionalAsync(ProfissionalSaude profissionalSaude);
        Task<ProfissionalSaude> EditarProfissionaisAsync(ProfissionalSaude profissional);
        Task<bool> DesativarProfissional(int id);
        Task<IEnumerable<ProfissionalSaude>> ListarProfissionaisAsync();

        // gestão de pacientes

        Task<Paciente> EditarPacinteAsync(Paciente paciente);
        Task<bool> DesativarPaciente(int id);
        Task<IEnumerable<Paciente>> ListarPacienteAsync();

        // consultas gerais
        Task<IEnumerable<Consulta>> ListarConsultasAsync();
        Task<bool> CancelarConsultas(int consultaid);

        //segurança e controle

        Task<bool> ResetarSenhaUsuariosAsync(int usuarioId);
        Task<bool> AtribuirPermissaoAsync(int usuarioId, string permissao);
    }
}
