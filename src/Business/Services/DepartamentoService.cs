using Business.Interfaces;
using Business.Interfaces.Repository;
using Business.Interfaces.Service;
using Business.Models;
using Business.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Business.Services
{
    public class DepartamentoService : BaseService<Departamento>, IDepartamentoService
    {
        private readonly IDepartamentoRepository _departamentoRepository;
        private readonly IEmprDeptRepository _emprDeptRepository;

        public DepartamentoService(IDepartamentoRepository departamentoRepository,
                                   IEmprDeptRepository emprDeptRepository,
                                   INotificador notificador): base(notificador)
        {
            _departamentoRepository = departamentoRepository;
            _emprDeptRepository = emprDeptRepository;
        }

        public async Task Adicionar(Departamento entity)
        {
            if (!IsValid(entity))
                return;

            await _departamentoRepository.Adicionar(entity);
        }

        public async Task Alterar(Departamento entity)
        {
            if (!IsValid(entity))
                return;

            await AdicionarRelacionamentoEmprDepto(entity);
            await _departamentoRepository.Alterar(entity);
        }

        public async Task Excluir(Guid id)
        {
            await _emprDeptRepository.ExcluirRelacionamentoByDetp(id);

            var entity = await _departamentoRepository.PesquisarId(id);
            await _departamentoRepository.Excluir(entity);
        }

        private async Task AdicionarRelacionamentoEmprDepto(Departamento entity)
        {
            var relac = entity.EmpresasDepartamento.ToList();
            await _emprDeptRepository.ExcluirRelacionamentoByDetp(entity.Id);
            await _emprDeptRepository.AdicionarLista(relac);
        }

        public async Task<IEnumerable<Departamento>> ObterTodos()
        {
            return await _departamentoRepository.GetDepartamentoComEmpresa();
        }

        public Task<IEnumerable<Departamento>> Pesquisar(Expression<Func<Departamento, bool>> pesquisa)
        {
            return _departamentoRepository.Pesquisar(pesquisa);
        }

        public Task<Departamento> PesquisarId(Guid id)
        {
            return _departamentoRepository.GetDepartamentoComEmpresa(id);
        }

        #region Validações
        public bool ExisteDescricao(Departamento dpto)
        {
            return _departamentoRepository.ExisteDescricao(dpto);
        }

        private bool ExisteCodigo(Departamento dpto)
        {
            return _departamentoRepository.ExisteCodigo(dpto);
        }

        private bool DepartamentoTemEmpresa(Departamento dpto)
        {
            return dpto.EmpresasDepartamento.Any();
        }

        private bool IsValid(Departamento entity)
        {
            if (!ExecutarValidacao(new DepartamentoValidation(), entity))
                return false;

            if (ExisteCodigo(entity))
            {
                Notificar("Já existe um departamento com este código!");
                return false;
            }

            if (ExisteDescricao(entity))
            {
                Notificar("Já existe um departamento com esta descrição!");
                return false;
            }

            if (!DepartamentoTemEmpresa(entity))
            {
                Notificar("É obrigatório que o departamento pertença ao menos á uma empresa!");
                return false;
            }

            return true;
        }
        #endregion
    }
}
