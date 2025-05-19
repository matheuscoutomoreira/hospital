using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace serviçohospital.Models;

public class Paciente
{
    [Key]
   public int Id { get; set; }
    [Required]
    [MaxLength(100)]
    public string Nome { get; set; }
    [Required]
    [MaxLength(14)]
    public string CPF { get; set; }
    [Required]
    public DateTime DataNascimento { get; set; }
    [Required]
    [MaxLength(15)]
    public string Telefone { get; set; }
    
    [JsonIgnore]
    public List<Consulta> Consultas { get; set; } = new List<Consulta>();
    
    [JsonIgnore]
    public virtual Historico? Historico { get; set; }  
}
