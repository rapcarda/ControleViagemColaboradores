using Business.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Business.Interfaces.Service
{
    public interface ICidadeService: IBaseService<Cidade>
    {
        Task Adicionar(Cidade entity);
        Task Alterar(Cidade entity);
        Task Excluir(Guid id);

        Task<IEnumerable<Cidade>> ObterCidadesEstadosPaises();
        Task<Cidade> PesquisarId(Guid id);
        Task<List<Cidade>> ObterTodos();
        Task<IEnumerable<Cidade>> Pesquisar(Expression<Func<Cidade, bool>> pesquisa);

        bool ExisteDescricao(Cidade entity);
    }
}
