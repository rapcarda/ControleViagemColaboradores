using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace API.ViewModel
{
    public class VeiculoViewModel: BaseViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(15, ErrorMessage = "O campo {0} pode ter no máximo {1} caracteres!")]
        [DisplayName("Código")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} pode ter no máximo {1} caracteres!")]
        [DisplayName("Modelo")]
        public string Modelo { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(300, ErrorMessage = "O campo {0} pode ter no máximo {1} caracteres!")]
        [DisplayName("Placa")]
        public string Placa { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public Guid EmpresaId { get; set; }

        [ScaffoldColumn(false)]
        public string NomeEmpresa { get; set; }
    }
}
