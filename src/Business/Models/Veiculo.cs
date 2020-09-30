using System;

namespace Business.Models
{
    public class Veiculo: Entity
    {
        public string Codigo { get; set; }
        public string Modelo { get; set; }
        public string Placa { get; set; }
        public Guid EmpresaId { get; set; }
        public Empresa Empresa { get; set; }
    }
}
