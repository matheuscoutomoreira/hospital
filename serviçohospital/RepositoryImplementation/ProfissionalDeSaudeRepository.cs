using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using serviçohospital.Context;
using serviçohospital.Models;
using serviçohospital.Repository;

namespace serviçohospital.RepositoryImplementation
{
    public class ProfissionalDeSaudeRepository : IProfissionalDesaudeRepository
    {

        private readonly AppDbContext _context;

        public ProfissionalDeSaudeRepository(AppDbContext context)
        {
            _context = context;
        }

      

        public async Task<ProfissionalSaude> Cretaasync(ProfissionalSaude profissional)
        {
           _context.Profissionais.AddAsync(profissional);
            await _context.SaveChangesAsync();
            return profissional;
        }

        public async Task<ProfissionalSaude> UpdateAsync(ProfissionalSaude profissional)
        {
            _context.Profissionais.Update(profissional);
            await _context.SaveChangesAsync();
            return profissional;
        }

        public async Task<ProfissionalSaude> DeleteAsync(int id)
        {
           var profissional = _context.Profissionais.Find(id);
            if (profissional == null) return null;
             _context.Profissionais.Remove(profissional);
            await _context.SaveChangesAsync();
            return profissional;
        }

       
        

        public async Task<bool> ExisteregistroAsync(string registro)
        {
            return await _context.Profissionais.AnyAsync( x => x.CRM == registro);
           
            
        }

        public async Task<IEnumerable<ProfissionalSaude>> GetAllAsync()
        {
            return await _context.Profissionais.ToListAsync();
        }

        public async Task<ProfissionalSaude> GetByIdAsync(int id)
        {
            return await _context.Profissionais.FindAsync(id);
        }

      


    }
}
