using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace API.ViewModel
{
    public class PaisViewModel: BaseViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} pode ter no máximo {1} caracteres!")]
        [DisplayName("Descrição")]
        public string Descricao { get; set; }

        public IEnumerable<EstadoViewModel> Estados { get; set; }
    }
}
