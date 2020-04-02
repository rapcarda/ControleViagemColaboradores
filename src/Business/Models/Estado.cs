using System;
using System.Collections.Generic;

namespace Business.Models
{
    public class Estado : Entity
    {
        public string Descricao { get; set; }
        public string UF { get; set; }
        /* Indicador de Chave Estrangeira para o EF */
        public Guid PaisId { get; set; }

        /* Relacionamento do EF, 1 estado pertence a 1 pais */
        public Pais Pais { get; set; }

        public IEnumerable<Cidade> Cidades { get; set; }
    }
}
