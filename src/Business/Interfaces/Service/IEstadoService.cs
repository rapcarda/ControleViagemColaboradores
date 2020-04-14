using Business.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Business.Interfaces.Service
{
    public interface IEstadoService: IBaseService<Estado>
    {
        Task Adicionar(Estado entity);
        Task Alterar(Estado entity);
        Task Excluir(Guid id);

        Task<Estado> ObterEstadoCidades(Guid id);
        Task<Estado> PesquisarId(Guid id);
        Task<List<Estado>> ObterTodos();
        Task<IEnumerable<Estado>> Pesquisar(Expression<Func<Estado, bool>> pesquisa);

        bool ExisteCodigo(Estado entity);
        bool ExisteDescricao(Estado entity);
    }
}
