using Business.Interfaces;
using Business.Interfaces.Repository;
using Business.Interfaces.Service;
using Business.Models;
using Business.Validation;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Business.Services
{
    public class EstadoService : BaseService<Estado>, IEstadoService
    {
        private readonly IEstadoRepository _estadoRepository;
        private readonly IPaisRepository _paisRespository;
        private readonly ICidadeRepository _cidadeRepository;

        public EstadoService(IEstadoRepository estadoRepository,
                             IPaisRepository paisRespository,
                             ICidadeRepository cidadeRepository,
                             INotificador notificador): base(notificador)
        {
            _estadoRepository = estadoRepository;
            _paisRespository = paisRespository;
            _cidadeRepository = cidadeRepository;
        }

        public async Task Adicionar(Estado entity)
        {
            if (!IsValid(entity))
                return;

            await _estadoRepository.Adicionar(entity);
        }

        public async Task Alterar(Estado entity)
        {
            if (!IsValid(entity))
                return;

            await _estadoRepository.Alterar(entity);
        }

        public async Task Excluir(Guid id)
        {
            if (_cidadeRepository.ExisteEstadoVinculado(id))
            {
                Notificar("Existem cidades vinculadas ao estado. Exclusão não permitida.");
            }
            else
            {
                var entity = await _estadoRepository.PesquisarId(id);
                await _estadoRepository.Excluir(entity);
            }
        }

        public async Task<Estado> ObterEstadoCidades(Guid id)
        {
            return await _estadoRepository.ObterEstadoCidades(id);
        }

        public async Task<List<Estado>> ObterTodos()
        {
            return await _estadoRepository.ObterTodos();
        }

        public async Task<IEnumerable<Estado>> Pesquisar(Expression<Func<Estado, bool>> pesquisa)
        {
            return await _estadoRepository.Pesquisar(pesquisa);
        }

        public async Task<Estado> PesquisarId(Guid id)
        {
            return await _estadoRepository.PesquisarId(id);
        }

        public async Task<IEnumerable<Estado>> ObterEstadosPaises()
        {
            return await _estadoRepository.ObterEstadosPaises();
        }

        public bool ExisteDescricao(Estado entity)
        {
            return _estadoRepository.ExisteDescricao(entity);
        }

        private bool IsValid(Estado entity)
        {
            if (!ExecutarValidacao(new EstadoValidation(), entity))
                return false;

            if (ExisteDescricao(entity))
            {
                Notificar("Já existe um Estado com a mesma descrição");
                return false;
            }

            if (!_paisRespository.ExistePais(entity.PaisId))
            {
                Notificar("País não cadastrado!");
                return false;
            }

            return true;
        }
    }
}
