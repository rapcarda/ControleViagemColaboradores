using Business.Interfaces;
using Business.Models;
using Business.Validation;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Business.Services
{
    public class PaisService : BaseService<Pais>, IPaisService
    {
        protected readonly IPaisRepository _paisRepository;

        public PaisService(IPaisRepository paisRepository,
                           INotificador notificador) : base(notificador)
        {
            _paisRepository = paisRepository;
        }

        public async Task Adicionar(Pais entity)
        {
            if (!ExecutarValidacao(new PaisValidation(), entity))
                return;

            if (!IsValid(entity))
                return;

            await _paisRepository.Adicionar(entity);
        }

        public async Task Alterar(Pais entity)
        {
            if (!ExecutarValidacao(new PaisValidation(), entity))
                return;

            if (!IsValid(entity))
                return;

            await _paisRepository.Alterar(entity);
        }

        public async Task Excluir(Guid id)
        {
            var entity = await _paisRepository.PesquisarId(id);
            await _paisRepository.Excluir(entity);
        }

        public async Task<Pais> ObterPaisEstados(Guid id)
        {
            return await _paisRepository.ObterPaisEstados(id);
        }

        public async Task<List<Pais>> ObterTodos()
        {
            return await _paisRepository.ObterTodos();
        }

        public async Task<IEnumerable<Pais>> Pesquisar(Expression<Func<Pais, bool>> pesquisa)
        {
            return await _paisRepository.Pesquisar(pesquisa);
        }

        public async Task<Pais> PesquisarId(Guid id)
        {
            return await _paisRepository.PesquisarId(id);
        }

        public bool ExisteDescricao(Pais entity)
        {
            return _paisRepository.ExisteDescricao(entity);
        }

        private bool IsValid(Pais entity)
        {
            if (ExisteDescricao(entity))
            {
                Notificar("Já existe um País com a mesma descrição");
                return false;
            }

            return true;
        }
    }
}
