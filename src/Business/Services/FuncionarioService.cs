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
    public class FuncionarioService: BaseService<Funcionario>, IFuncionarioService
    {
        private readonly IFuncionarioRepository _funcionarioRepository;
        private readonly ICidadeRepository _cidadeRepository;
        private readonly IDepartamentoRepository _departamentoRepository;

        public FuncionarioService(IFuncionarioRepository funcionarioRepository,
                                  ICidadeRepository cidadeRepository,
                                  IDepartamentoRepository departamentoRepository,
                                  INotificador notificador): base(notificador)
        {
            _funcionarioRepository = funcionarioRepository;
            _cidadeRepository = cidadeRepository;
            _departamentoRepository = departamentoRepository;
        }

        public async Task Adicionar(Funcionario entity)
        {
            if (!IsValid(entity))
                return;

            await _funcionarioRepository.Adicionar(entity);
        }

        public async Task Alterar(Funcionario entity)
        {
            if (!IsValid(entity))
                return;

            await _funcionarioRepository.Alterar(entity);
        }

        public async Task Excluir(Guid id)
        {
            var func = await _funcionarioRepository.PesquisarId(id);
            await _funcionarioRepository.Excluir(func);
        }

        public async Task<IEnumerable<Funcionario>> ObterTodos()
        {
            return await _funcionarioRepository.ObterTodos();
        }

        public bool ExisteCodigo(Funcionario funcionario)
        {
            return _funcionarioRepository.ExisteCodigo(funcionario);
        }

        public bool ExisteDescricao(Funcionario funcionario)
        {
            return _funcionarioRepository.ExisteDescricao(funcionario);
        }

        public async Task<IEnumerable<Funcionario>> GetFuncionarioComCidadeEDepto()
        {
            return await _funcionarioRepository.GetFuncionarioComCidadeEDepto();
        }

        public async Task<IEnumerable<Funcionario>> Pesquisar(Expression<Func<Funcionario, bool>> pesquisa)
        {
            return await _funcionarioRepository.Pesquisar(pesquisa);
        }

        public async Task<Funcionario> PesquisarId(Guid id)
        {
            return await _funcionarioRepository.PesquisarId(id);
        }

        public bool ExisteCidade(Funcionario funcionario)
        {
            return _cidadeRepository.ExistePorId(funcionario.CidadeId);
        }

        public bool ExisteDepartamento(Funcionario funcionario)
        {
            return _departamentoRepository.ExistePorId(funcionario.DepartamentoId);
        }

        private bool IsValid(Funcionario entity)
        {
            if (!ExecutarValidacao(new FuncionarioValidation(), entity))
                return false;

            if (ExisteCodigo(entity))
            {
                Notificar("Já existe um Funcionário com o mesmo código");
                return false;
            }

            if (ExisteDescricao(entity))
            {
                Notificar("Já existe um Funcionário com o mesmo nome");
                return false;
            }

            if (!ExisteCidade(entity))
            {
                Notificar("Cidade não cadastrada.");
                return false;
            }

            if (!ExisteDepartamento(entity))
            {
                Notificar("Departamento não cadastrado.");
                return false;
            }

            return true;
        }
    }
}
