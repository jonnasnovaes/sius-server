using System;

namespace sius_server.Models
{
    public class Vacina : BaseEntity
    {
        public string Nome { get; set; }
        public string Fabricante { get; set; }
        public string DataFabricacao { get; set; }
        public int NumeroRegistro { get; set; }
    }
}