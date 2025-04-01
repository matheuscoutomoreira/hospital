using System.ComponentModel.DataAnnotations;

namespace serviçohospital.Models;

public class ProfissionalSaude
{
    [Required]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string? Nome { get; set; }

    [Required]
    [MaxLength(10)]
    public string? CRM { get;set; }

    [Required]
    [MaxLength(50)]
    public string Especialidade { get; set; }


    // relacionamento com a classe consulta 
    public List<Consulta> Consultas { get; set; } = new List<Consulta>();// inicializando a lista para evitar null
}
