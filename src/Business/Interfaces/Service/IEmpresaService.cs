using Business.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Business.Interfaces.Service
{
    public interface IEmpresaService: IBaseService<Empresa>
    {
        Task Adicionar(Empresa entity);
        Task Alterar(Empresa entity);
        Task Excluir(Guid id);

        Task<IEnumerable<Empresa>> GetEmpresaComCidade();
        Task<Empresa> PesquisarId(Guid id);
        Task<List<Empresa>> ObterTodos();
        Task<IEnumerable<Empresa>> Pesquisar(Expression<Func<Empresa, bool>> pesquisa);

        bool ExisteCodigo(Empresa empresa);
        bool ExisteDescricao(Empresa empresa);
    }
}
