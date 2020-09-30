using Business.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Validation
{
    public class VeiculoValidation: AbstractValidator<Veiculo>
    {
        public VeiculoValidation()
        {
            RuleFor(r => r.Codigo)
                .NotNull().WithMessage("O campo {PropertyName} precisa ser fornecido!")
                .Length(1, 15).WithMessage("O campo {PropertyName} precisa ser entre {MinLength} e {MaxLength} caracteres!");

            RuleFor(r => r.Modelo)
                .NotNull().WithMessage("O campo {PropertyName} precisa ser fornecido!")
                .Length(2, 100).WithMessage("O campo {PropertyName} precisa ser entre {MinLength} e {MaxLength} caracteres!");

            RuleFor(r => r.Placa)
                .NotNull().WithMessage("O campo {PropertyName} precisa ser fornecido!")
                .Length(2, 10).WithMessage("O campo {PropertyName} precisa ser entre {MinLength} e {MaxLength} caracteres!");
        }
    }
}
