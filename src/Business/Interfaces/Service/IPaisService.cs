using Business.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IPaisService : IBaseService<Pais>
    {
        Task Adicionar(Pais entity);
        Task Alterar(Pais entity);
        Task Excluir(Guid id);

        Task<Pais> ObterPaisEstados(Guid id);
        Task<Pais> PesquisarId(Guid id);
        Task<List<Pais>> ObterTodos();
        Task<IEnumerable<Pais>> Pesquisar(Expression<Func<Pais, bool>> pesquisa);

        bool ExisteDescricao(Pais entity);
    }
}
