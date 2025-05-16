using serviçohospital.Models;

namespace serviçohospital.Repository
{
    public interface IProfissionalDesaudeRepository
    {

        /// crud basico
        
         Task<ProfissionalSaude>GetByIdAsync(int id);
        Task< List<Consulta>> ObterAgenda(int profissionalId);
         Task<Consulta> FinalizarConsulta(int consultaId, string prontuario);
        Task<Prescricao> EmitirPrescricao(int consultaId, string medicamento, string dosagem);
         Task<List<Consulta>> ObterHistoricoPaciente(int pacienteId);


       

        //atualiza o proprio cadastro

       

        // verifica se ja existe um crm ou cpf
        
        
    }
}
