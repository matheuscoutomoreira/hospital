using serviçohospital.Models;
using System.Text.Json.Serialization;

public class Historico
{
    public int Id { get; set; }

    public int PacienteId { get; set; }
    public virtual Paciente Paciente { get; set; }

    public int ProfissionalSaudeId { get; set; }
    public virtual ProfissionalSaude ProfissionalSaude { get; set; }

    public DateTime Data { get; set; }
    public string Descricao { get; set; }
    public string TipoEvento { get; set; }
    public string Observacoes { get; set; }

    // Relacionamento com prescrições
    [JsonIgnore]
    public virtual ICollection<Prescricao> Prescricoes { get; set; } = new List<Prescricao>();

    // Relacionamento com consultas (se quiser incluir no histórico)
    public virtual ICollection<Consulta> Consultas { get; set; } = new List<Consulta>();
}
