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
        Task<IEnumerable<ProfissionalSaude>> GetAllAsync();

        //atualiza o proprio cadastro

       

        // verifica se ja existe um crm ou cpf
        
        Task<bool> ExisteregistroAsync(string registro);//CRM
    }
}
