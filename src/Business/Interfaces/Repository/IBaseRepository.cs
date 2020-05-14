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
        Task AdicionarLista(IEnumerable<TEntity> listEntity);
        Task Alterar(TEntity entity);
        Task Excluir(TEntity entity);

        Task<TEntity> PesquisarId(Guid id);
        bool ExistePorId(Guid id);
        Task<List<TEntity>> ObterTodos();
        Task<IEnumerable<TEntity>> Pesquisar(Expression<Func<TEntity, bool>> pesquisa);
        Task<int> SaveChanges();
    }
}
