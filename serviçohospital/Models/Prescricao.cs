using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace serviçohospital.Models;

public class Prescricao
{
    [Key]
    public int ID { get; set; }

    [Required]
    public int ConsultaID { get; set; }

    [ForeignKey("ConsultaID")]
    public Consulta consulta { get;set; }


    [Required]
    [MaxLength(200)]
    public string Medicamento { get; set; }

    [Required]
    [MaxLength(100)]
    public string? Dosagem { get; set; }
    public DateTime DataPrescricao { get; set; } = DateTime.UtcNow;
}
