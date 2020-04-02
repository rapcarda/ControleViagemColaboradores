using Business.Models;
using FluentValidation;

namespace Business.Validation
{
    public class PaisValidation : AbstractValidator<Pais>
    {
        public PaisValidation()
        {
            RuleFor(r => r.Descricao)
                .NotNull().WithMessage("O campo {PropertyName} precisa ser fornecido!")
                .Length(2, 100).WithMessage("O campo {PropertyName} precisa ser entre {MinLength} e {MaxLength} caracteres!");
        }
    }
}
