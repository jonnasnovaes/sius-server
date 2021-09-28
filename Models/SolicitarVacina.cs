using System;

namespace sius_server.Models
{
    public class SolicitarVacina : BaseEntity
    {
        public int idVacina { get; set; }
        public Boolean liberado { get; set; }
        public Boolean recebido { get; set; }
    }
}