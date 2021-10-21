namespace sius_server.Models
{
    public class EstoqueVacina: BaseEntity
    {
        public int IdVacina { get; set; }
        public string Nome { get; set; }
        public string Fabricante { get; set; }
        public int Quantidade { get; set; }
        public string Bula { get; set; }
    }
}