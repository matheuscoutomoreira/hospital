using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace serviçohospital.Models
{
    public enum StatusConsulta
    {
        Agendado,
        Cancelada,
        Feita
    }

    public class Consulta
    {
        [Key]
        public int Id { get; set; }

        public int PacienteId { get; set; }
        public virtual Paciente Paciente { get; set; }

        public int ProfissionalSaudeId { get; set; }
        public virtual ProfissionalSaude Profissional { get; set; }

        public int? HistoricoId { get; set; }  // Pode ser opcional
        public virtual Historico Historico { get; set; }

        [Required]
        public DateTime DataHora { get; set; } = DateTime.Now;

        [Required]
        public StatusConsulta Status { get; set; }

        public string? Prontuario { get; set; }
        [JsonIgnore]
        public virtual ICollection<Prescricao> Prescricoes { get; set; } = new List<Prescricao>();
    }
}
