namespace sius_server.Models
{
    public class RegistrarVacinacao : BaseEntity
    {
        public string Nome { get; set; }
        public string NumeroSus { get; set; }
        public int Idade { get; set; }
        public string Vacina { get; set; }
        public string DataVacinacao { get; set; }
    }
}