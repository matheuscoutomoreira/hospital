using serviçohospital.Models;

namespace serviçohospital.Repository
{
    public interface IProfissionalDesaudeRepository
    {

        /// crud basico
        Task<ProfissionalSaude> Createasync(ProfissionalSaude profissional);
        Task<ProfissionalSaude> UpdateAsync(ProfissionalSaude profissional);
        Task<ProfissionalSaude> DeleteAsync(int id);
         Task<ProfissionalSaude>GetByIdAsync(int id);
        Task< List<Consulta>> ObterAgenda(int profissionalId);
         void FinalizarConsulta(int consultaId, string prontuario);
        void EmitirPrescricao(int consultaId, string medicamento, string dosagem);
         Task<List<Consulta>> ObterHistoricoPaciente(int pacienteId);


       

        //atualiza o proprio cadastro

       

        // verifica se ja existe um crm ou cpf
        
        Task<bool> ExisteregistroAsync(string registro);//CRM
    }
}
