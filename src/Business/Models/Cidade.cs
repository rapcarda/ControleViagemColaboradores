using System;
using System.Collections.Generic;

namespace Business.Models
{
    public class Cidade : Entity
    {
        public string Descricao { get; set; }
        public Guid EstadoId { get; set; }
        public Estado Estado { get; set; }
        public IEnumerable<Empresa> Empresas{ get; set; }
    }
}
