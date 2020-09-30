using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace API.ViewModel
{
    public class FuncionarioViewModel : BaseViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(15, ErrorMessage = "O campo {0} pode ter no máximo {1} caracteres!")]
        [DisplayName("Código")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} pode ter no máximo {1} caracteres!")]
        [DisplayName("Nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(300, ErrorMessage = "O campo {0} pode ter no máximo {1} caracteres!")]
        [DisplayName("Logradouro")]
        public string Logradouro { get; set; }

        [DisplayName("Bairro")]
        public string Bairro { get; set; }

        [DisplayName("Complemento")]
        public string Complemento { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(8, ErrorMessage = "O campo {0} pode ter no máximo {1} caracteres!")]
        [DisplayName("CEP")]
        public string Cep { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public Guid CidadeId { get; set; }

        [ScaffoldColumn(false)]
        public string NomeCidade { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public Guid DepartamentoId { get; set; }

        [ScaffoldColumn(false)]
        public string NomeDepartamento { get; set; }
    }
}
