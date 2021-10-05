namespace sius_server.Models
{
    public class Login : BaseEntity
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        
        public int Perfil { get; set; }
    }
}