using System.ComponentModel.DataAnnotations;

namespace serviçohospital.Models;

public class Administracao
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(100)]
    public string Nome { get; set; }
    [Required]
    public string Cargo { get; set; }
}
