using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace serviçohospital.Models
{
    public class Usuario
    {
        [Required]
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Nome { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [StringLength(50)]
        [NotNull]
        public string Senha { get; set; } // Guardar de forma segura com hash
        
        public TipoUsuario Tipo { get; set; } // Define o perfil do usuário
        //Tipo do Usuário (campo "tipo"):
        //- 0: Paciente
        //- 1: Profissional da Saúde
        //- 2: Administrador

        // Relacionamentos
        public Paciente? Paciente { get; set; }
        public ProfissionalSaude? ProfissionalSaude {get;set;}
        public Administracao? Administracao { get; set; }
    } }

    // Enum para definir os tipos de usuários
    public enum TipoUsuario
    {
        Paciente,
        Profissional,
        Administrador
    }

