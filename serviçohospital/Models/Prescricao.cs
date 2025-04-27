using serviçohospital.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class Prescricao
{
    [Key]
    public int ID { get; set; }

    [Required]
    public int ConsultaID { get; set; }

    [ForeignKey("ConsultaID")]
    public Consulta consulta { get; set; }

    [Required]
    [MaxLength(200)]
    public string Medicamento { get; set; }

    [MaxLength(100)]
    public string? Dosagem { get; set; }

    public DateTime DataPrescricao { get; set; } = DateTime.UtcNow;

    // Relacionamento com Histórico (caso necessário)
    public int HistoricoId { get; set; }
    [ForeignKey("HistoricoId")]
    public virtual Historico Historico { get; set; }
}
