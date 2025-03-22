using System.ComponentModel.DataAnnotations;

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
        [Required]
        int id { get; set; }
        int PacientID { get;set; }
        int ProfissionalID { get; set; }
        DateTime DataHora { get;set; }
        StatusConsulta Status { get; set; }
        string? Prontuario { get; set; }
    }
}
