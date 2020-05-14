using System;
using System.Collections.Generic;

namespace Business.Models
{
    public class Departamento: Entity
    {
        public Departamento()
        {
            EmpresasDepartamento = new List<EmprDept>();
        }

        public string Codigo { get; set; }
        public string Nome { get; set; }

        /* EmpresaS do departamento */
        public virtual ICollection<EmprDept> EmpresasDepartamento { get; set; }
    }
}
