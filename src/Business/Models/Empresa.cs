using System;

namespace Business.Models
{
    public class Empresa: Entity
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public Guid CidadeId { get; set; }
        public Cidade Cidade { get; set; }
    }
}
