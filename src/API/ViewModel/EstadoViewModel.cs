﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace API.ViewModel
{
    public class EstadoViewModel : BaseViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} pode ter no máximo {1} caracteres!")]
        [DisplayName("Descrição")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(2, ErrorMessage = "O campo {0} pode ter no máximo {1} caracteres!")]
        public string UF { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public Guid PaisId { get; set; }

        [ScaffoldColumn(false)]
        public string NomePais { get; set; }

        public IEnumerable<CidadeViewModel> Cidades { get; set; }
    }
}
