using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace serviçohospital.Models;




   public enum StatusConsulta
{
    Agendado,
    Cancelada,
    Feita
}
public class Consulta
{
    
    [Key]
     public int id { get; set; }
    [ForeignKey("PacienteId")]
    public int PacientID { get;set; }
    [Required]
    public int ProfissionalID { get; set; }
    [ForeignKey("ProfissionalId")]
    public ProfissionalSaude Profissional { get; set; }
    [Required]
    public DateTime DataHora { get; set; } = DateTime.Now;
    [Required]
    public StatusConsulta Status { get; set; }
    public string? Prontuario { get; set; }
}
