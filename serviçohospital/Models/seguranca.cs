using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace serviçohospital.Models
{
    public class Seguranca
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("UsuarioID")]
        public int UsuarioId { get; set; }
        [Required]
        public string Acao { get; set; }
        [Required]
        public DateTime DataHora { get; set; }

        public Usuario Usuario { get; set; }
    }

}
