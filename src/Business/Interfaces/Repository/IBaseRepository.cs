using Business.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IBaseRepository<TEntity> : IDisposable where TEntity : Entity
    {
        Task Adicionar(TEntity entity);
        Task Alterar(TEntity entity);
        Task Excluir(Guid id);

        Task<TEntity> PesquisarId(Guid id);
        bool ExisteEntidade(Guid id);
        Task<List<TEntity>> ObterTodos();
        Task<IEnumerable<TEntity>> Pesquisar(Expression<Func<TEntity, bool>> pesquisa);
        Task<int> SaveChanges();
    }
}
