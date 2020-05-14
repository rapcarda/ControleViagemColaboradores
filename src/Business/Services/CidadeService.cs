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
    public class CidadeService : BaseService<Cidade>, ICidadeService
    {
        private ICidadeRepository _cidadeRepository;
        private IEstadoRepository _estadoRespository;

        public CidadeService(ICidadeRepository cidadeRespository,
                             IEstadoRepository estadoRepository,
                             INotificador notificador): base(notificador)
        {
            _cidadeRepository = cidadeRespository;
            _estadoRespository = estadoRepository;
        }

        public async Task Adicionar(Cidade entity)
        {
            if (!IsValid(entity))
                return;

            await _cidadeRepository.Adicionar(entity);
        }

        public async Task Alterar(Cidade entity)
        {
            if (!IsValid(entity))
                return;

            await _cidadeRepository.Alterar(entity);
        }

        public async Task Excluir(Guid id)
        {
            var entity = await _cidadeRepository.PesquisarId(id);
            await _cidadeRepository.Excluir(entity);
        }

        public async Task<IEnumerable<Cidade>> ObterCidadesEstadosPaises()
        {
            return await _cidadeRepository.ObterCidadesEstadosPaises();
        }

        public async Task<List<Cidade>> ObterTodos()
        {
            return await _cidadeRepository.ObterTodos();
        }

        public async Task<IEnumerable<Cidade>> Pesquisar(Expression<Func<Cidade, bool>> pesquisa)
        {
            return await _cidadeRepository.Pesquisar(pesquisa);
        }

        public async Task<Cidade> PesquisarId(Guid id)
        {
            return await _cidadeRepository.PesquisarId(id);
        }

        public bool ExisteDescricao(Cidade entity)
        {
            return _cidadeRepository.ExisteDescricao(entity);
        }

        private bool VerificarEstadoExiste(Guid id)
        {
            return _estadoRespository.ExisteEstado(id);
        }

        private bool IsValid(Cidade entity)
        {
            if (!ExecutarValidacao(new CidadeValidation(), entity))
                return false;

            if (ExisteDescricao(entity))
            {
                Notificar("Já existe uma Cidade com a mesma descrição");
                return false;
            }

            if (!VerificarEstadoExiste(entity.EstadoId))
            {
                Notificar("Estado não cadastrado!");
                return false;
            }

            return true;
        }
    }
}
