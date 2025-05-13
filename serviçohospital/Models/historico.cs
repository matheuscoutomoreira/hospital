using serviçohospital.Models;

public class Historico
{
    public int Id { get; set; }
    public int PacienteId { get; set; }
    public DateTime Data { get; set; }
    public virtual Paciente Paciente { get; set; }

    // Relacionamento com as consultas
    public virtual ICollection<Consulta> Consultas { get; set; } = new List<Consulta>();
}