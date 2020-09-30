using Business.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Business.Interfaces.Service
{
    public interface IFuncionarioService: IBaseService<Funcionario>
    {
        Task Adicionar(Funcionario entity);
        Task Alterar(Funcionario entity);
        Task Excluir(Guid id);

        Task<IEnumerable<Funcionario>> GetFuncionarioComCidadeEDepto();
        Task<Funcionario> PesquisarId(Guid id);
        Task<IEnumerable<Funcionario>> Pesquisar(Expression<Func<Funcionario, bool>> pesquisa);
        Task<IEnumerable<Funcionario>> ObterTodos();

        bool ExisteCodigo(Funcionario funcionario);
        bool ExisteDescricao(Funcionario funcionario);
    }
}
