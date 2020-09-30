using Business.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Validation
{
    public class FuncionarioValidation: AbstractValidator<Funcionario>
    {
        public FuncionarioValidation()
        {
            RuleFor(r => r.Codigo)
                .NotNull().WithMessage("O campo {PropertyName} precisa ser fornecido!")
                .Length(1, 15).WithMessage("O campo {PropertyName} precisa ser entre {MinLength} e {MaxLength} caracteres!");

            RuleFor(r => r.Nome)
                .NotNull().WithMessage("O campo {PropertyName} precisa ser fornecido!")
                .Length(2, 100).WithMessage("O campo {PropertyName} precisa ser entre {MinLength} e {MaxLength} caracteres!");

            RuleFor(r => r.Logradouro)
                .NotNull().WithMessage("O campo {PropertyName} precisa ser fornecido!")
                .Length(2, 300).WithMessage("O campo {PropertyName} precisa ser entre {MinLength} e {MaxLength} caracteres!");

            RuleFor(c => c.Cep)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(8).WithMessage("O campo {PropertyName} precisa ter {MaxLength} caracteres");
        }
    }
}
