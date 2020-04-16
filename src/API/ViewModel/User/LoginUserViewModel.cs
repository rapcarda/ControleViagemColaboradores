using System.ComponentModel.DataAnnotations;

namespace API.ViewModel.User
{
    public class LoginUserViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [EmailAddress(ErrorMessage = "{0} está em formato errado!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(20, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres!", MinimumLength = 6)]
        public string Password { get; set; }
    }
}
