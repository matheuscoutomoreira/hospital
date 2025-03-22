namespace serviçohospital.Models
{
    public class Pacientes
    {
        int id { get; set; }
        string? Nome { get; set; }
        string? CPF { get; set; }
        DateTime DataNascimento { get; set; }
        string? Telefone { get; set; }
        // relacionamento com consulta (1/n)

        public List<Consulta> Consultas { get; set;}
    }
}
