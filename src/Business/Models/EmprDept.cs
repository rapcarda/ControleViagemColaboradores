using System;

namespace Business.Models
{
    public class EmprDept: Entity
    {
        public Guid EmpresaId { get; set; }
        public virtual Empresa Empresa { get; set; }
        public Guid DepartamentoId { get; set; }
        public virtual Departamento Departamento { get; set; }
    }
}
