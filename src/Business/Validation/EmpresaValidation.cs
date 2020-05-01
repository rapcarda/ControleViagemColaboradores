using Business.Models;
using FluentValidation;

namespace Business.Validation
{
    public class EmpresaValidation: AbstractValidator<Empresa>
    {
        public EmpresaValidation()
        {
            RuleFor(r => r.Codigo)
                .NotNull().WithMessage("O campo {PropertyName} precisa ser fornecido!")
                .Length(1, 15).WithMessage("O campo {PropertyName} precisa ser entre {MinLength} e {MaxLength} caracteres!");

            RuleFor(r => r.Nome)
                .NotNull().WithMessage("O campo {PropertyName} precisa ser fornecido!")
                .Length(2, 100).WithMessage("O campo {PropertyName} precisa ser entre {MinLength} e {MaxLength} caracteres!");
        }
    }
}
