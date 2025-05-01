using serviçohospital.Models;

namespace serviçohospital.Repository
{
    public interface IPacientesRepository
    {
       Task< Paciente> Create(Paciente paciente);
        Task<Paciente> Update(Paciente paciente);
        Task<Paciente> Delete(int id);
        // pegar dados do paciente
        Task<Paciente> GetByIdAsync(int id);
        Task<IEnumerable<Paciente>> GetAllAsync();
        Task<IEnumerable<Paciente>> BuscarPorNomeAsync(string nome);
        Task<Paciente> BuscarPorCpfAsync(string cpf);
        // criar consultas e ver hidtorico
        Task<Consulta> CriarConsultaAsync(Consulta consulta);
        Task<Historico> GetHistoricoAsync(int pacienteId);


    }
}
