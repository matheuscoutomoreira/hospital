namespace serviçohospital.Crypto
{
    public class LoginDTO
    {
        public string Email { get; set; }
        public string Senha { get; set; }
    }

    public class RegisterRequest
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public TipoUsuario Tipo { get; set; }
    }
}
