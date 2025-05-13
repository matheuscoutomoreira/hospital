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

        [Required]
        public DateTime DataHora { get; set; }

        [Required]
        public StatusConsulta Status { get; set; }

        // Detalhes da consulta
        public string TipoConsulta { get; set; }
        public string Observacoes { get; set; }

        public string? Prontuario { get; set; }

        // Relacionamento com prescrições
        [JsonIgnore]
        public virtual ICollection<Prescricao> Prescricoes { get; set; } = new List<Prescricao>();
    }



}
