using System.ComponentModel.DataAnnotations;

namespace serviçohospital.Models
{
    public class ProfissionalSaude
    {
        [Required]
        public int id { get; set; }
        public string? Nome { get; set; }
        public string? CRM { get;set; }
        public string Especialidade { get; set; }


        // relacionamento com a classe consulta 
        public List<Consulta> Consultas { get; set; }
    }
}
