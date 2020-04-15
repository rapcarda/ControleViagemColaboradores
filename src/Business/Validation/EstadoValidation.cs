using Business.Models;
using FluentValidation;

namespace Business.Validation
{
    class EstadoValidation : AbstractValidator<Estado>
    {
        public EstadoValidation()
        {
            RuleFor(r => r.Descricao)
                .NotNull().WithMessage("O campo {PropertyName} precisa ser fornecido!")
                .Length(2, 100).WithMessage("O campo {PropertyName} precisa ser entre {MinLength} e {MaxLength} caracteres!");

            RuleFor(r => r.UF)
                .NotNull().WithMessage("O campo {PropertyName} precisa ser fornecido!")
                .MaximumLength(2).WithMessage("O campo {PropertName} deve ter no máximo {MaxLength} caracteres!");

            RuleFor(r => r.PaisId)
                .NotNull().WithMessage("O campo {PropertyName} precisa ser fornecido!");
        }
    }
}
