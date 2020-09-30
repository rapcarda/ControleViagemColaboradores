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
    public class EmpresaService : BaseService<Empresa>, IEmpresaService
    {
        private readonly IEmpresaRepository _empresaRepository;
        private readonly ICidadeRepository _cidadeRepository;
        private readonly IDepartamentoRepository _departamentoRepository;

        public EmpresaService(IEmpresaRepository empresaRepository,
                              ICidadeRepository cidadeRepository,
                              IDepartamentoRepository departamentoRepository,
                              INotificador notificador): base(notificador)
        {
            _empresaRepository = empresaRepository;
            _cidadeRepository = cidadeRepository;
            _departamentoRepository = departamentoRepository;
        }

        public async Task Adicionar(Empresa entity)
        {
            if (!IsValid(entity))
                return;

            await _empresaRepository.Adicionar(entity);
        }

        public async Task Alterar(Empresa entity)
        {
            if (!IsValid(entity))
                return;

            await _empresaRepository.Alterar(entity);
        }

        public async Task Excluir(Guid id)
        {
            if (_departamentoRepository.ExisteEmpresaVinculado(id))
            {
                Notificar("Existem departamentos vinculados a empresa. Exclusão não permitida.");
            }
            else
            {
                var entity = await _empresaRepository.PesquisarId(id);
                await _empresaRepository.Excluir(entity);
            }
        }

        public async Task<IEnumerable<Empresa>> GetEmpresaComCidade()
        {
            return await _empresaRepository.GetEmpresaComCidade();
        }

        public async Task<List<Empresa>> ObterTodos()
        {
            return await _empresaRepository.ObterTodos();
        }

        public async Task<IEnumerable<Empresa>> Pesquisar(Expression<Func<Empresa, bool>> pesquisa)
        {
            return await _empresaRepository.Pesquisar(pesquisa);
        }

        public async Task<Empresa> PesquisarId(Guid id)
        {
            return await _empresaRepository.PesquisarId(id);
        }

        #region Validations
        public bool ExisteCodigo(Empresa empresa)
        {
            return _empresaRepository.ExisteCodigo(empresa);
        }

        public bool ExisteDescricao(Empresa empresa)
        {
            return _empresaRepository.ExisteDescricao(empresa);
        }

        public bool ExisteCidade(Empresa empresa)
        {
            return _cidadeRepository.ExistePorId(empresa.CidadeId);
        }

        private bool IsValid(Empresa entity)
        {
            if (!ExecutarValidacao(new EmpresaValidation(), entity))
                return false;

            if (ExisteCodigo(entity))
            {
                Notificar("Já existe uma Empresa com o mesmo código");
                return false;
            }

            if (ExisteDescricao(entity))
            {
                Notificar("Já existe uma Empresa com a mesma descrição");
                return false;
            }

            if (!ExisteCidade(entity))
            {
                Notificar("Cidade não cadastrada.");
                return false;
            }

            return true;
        }
        #endregion
    }
}
