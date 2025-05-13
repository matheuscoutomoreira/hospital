namespace serviçohospital.Models
{
    public class CriarConsultaDTO
    {
        public DateTime DataConsulta { get; set; }
        public int PacienteId { get; set; }
        public int ProfissionalSaudeId { get; set; }
        public string Observacoes { get; set; }
    }
}
