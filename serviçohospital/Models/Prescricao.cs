namespace serviçohospital.Models
{
    public class Prescricao
    {
        public int ID { get; set; }
        public int ConsultaID { get; set; }
        public Consulta consulta { get;set; }



        public string? Medicamento { get; set; }
        public string? Dosagem { get; set; }
        public DateTime DataPrescricao { get; set; }
    }
}
