﻿using serviçohospital.Models;

namespace serviçohospital.Repository
{
    public interface IPacientesRepository
    {
       Task< Paciente> CreateAsync(Paciente paciente);
        Task<Paciente> UpdateAsync(Paciente paciente);
        Task<Paciente> DeleteAsync(int id);
        // pegar dados do paciente
        Task<Consulta> GetConsultaByIdAsync( int id);
        Task<Paciente> GetByIdAsync(int id);
        Task<IEnumerable<Paciente>> GetAllAsync();
        Task<Paciente> BuscarPorNomeAsync(string nome);
        Task<Paciente> BuscarPorCpfAsync(string cpf);
        // criar consultas e ver hidtorico
        Task<Consulta> CriarConsultaAsync(Consulta consulta);
        public Task<Consulta> CancelarConsultaAsync(int consultaId);
        Task<Historico> GetHistoricoAsync(int pacienteId);

        Task<Historico> CriarHistoricoAsync(int pacienteId); // Novo método para criar histórico
        
    }
}
