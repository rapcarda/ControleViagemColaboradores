﻿using Business.Interfaces;
using Business.Models;
using Business.Notifications;
using FluentValidation;
using FluentValidation.Results;

namespace Business.Services
{
    public abstract class BaseService<TEntity> : IBaseService<TEntity> where TEntity : Entity
    {
        private readonly INotificador _notificador;

        public BaseService(INotificador notificador)
        {
            _notificador = notificador;
        }

        protected void Notificar(ValidationResult validationResult)
        {
            foreach(var val in validationResult.Errors)
            {
                Notificar(val.ErrorMessage);
            }
        }

        protected void Notificar(string mensagem)
        {
            _notificador.Handle(new Notificacao(mensagem));
        }

        protected bool ExecutarValidacao<TV, TE>(TV validacao, TE entidade) where TV : AbstractValidator<TE> where TE : Entity
        {
            var validator = validacao.Validate(entidade);

            if (validator.IsValid)
                return true;

            Notificar(validator);
            return false;
        }

        public void Dispose()
        {
            
        }
    }
}
