using System.Collections.Generic;

namespace Business.Models
{
    public class Pais : Entity
    {
        public string Descricao { get; set; }

        /* Indicador para o EF que 1 pais tem muitos estados */
        public IEnumerable<Estado> Estados { get; set; }
    }
}
