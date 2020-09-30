using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Models
{
    public class Funcionario: Entity
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Complemento { get; set; }
        public string Cep { get; set; }
        public Guid CidadeId { get; set; }
        public Cidade Cidade { get; set; }
        public Guid DepartamentoId { get; set; }
        public Departamento Departamento { get; set; }

    }
}
