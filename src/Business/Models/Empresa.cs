using System;
using System.Collections.Generic;

namespace Business.Models
{
    public class Empresa: Entity
    {
        public Empresa()
        {
            EmpresaDepartamentos = new List<EmprDept>();
        }

        public string Codigo { get; set; }
        public string Nome { get; set; }
        public Guid CidadeId { get; set; }
        public Cidade Cidade { get; set; }

        /* DepartamentoS da empresa */
        public virtual ICollection<EmprDept> EmpresaDepartamentos { get; set; }
    }
}
