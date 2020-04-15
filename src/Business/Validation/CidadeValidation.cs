using Business.Models;
using FluentValidation;

namespace Business.Validation
{
    public class CidadeValidation : AbstractValidator<Cidade>
    {
        public CidadeValidation()
        {
            RuleFor(r => r.Descricao)
                .NotNull().WithMessage("O campo {PropertyName} precisa ser fornecido!")
                .Length(2, 100).WithMessage("O campo {PropertyName} precisa ser entre {MinLength} e {MaxLength} caracteres!");
        }
    }
}
