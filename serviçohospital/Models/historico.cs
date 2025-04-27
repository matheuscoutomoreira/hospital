using serviçohospital.Models;

public class Historico
{
    public int Id { get; set; }
    public int PacienteId { get; set; }
    public DateTime Data { get; set; }
    public string Descricao { get; set; }
    public string TipoEvento { get; set; }
    public int MedicoId { get; set; }
    public string Observacoes { get; set; }

    // Relacionamento com as prescrições
    public virtual ICollection<Prescricao> Prescricoes { get; set; }

    public virtual Paciente Paciente { get; set; }
    public virtual ProfissionalSaude ProfissionalSaude { get; set; }
}

