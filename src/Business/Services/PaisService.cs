using Business.Interfaces;
using Business.Interfaces.Repository;
using Business.Models;
using Business.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Business.Services
{
    public class PaisService : BaseService<Pais>, IPaisService
    {
        protected readonly IPaisRepository _paisRepository;
        protected readonly IEstadoRepository _estadoRepository;

        public PaisService(IPaisRepository paisRepository,
                           IEstadoRepository estadoRepository,
                           INotificador notificador) : base(notificador)
        {
            _paisRepository = paisRepository;
            _estadoRepository = estadoRepository;
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
            if (_estadoRepository.ExistePaisVinculado(id))
            {
                Notificar("Existem estados vinculados ao país. Exclusão não permitida.");
            } 
            else
            {
                var entity = await _paisRepository.PesquisarId(id);
                await _paisRepository.Excluir(entity);
            }
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
