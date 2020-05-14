using Business.Interfaces;
using Business.Models;
using Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Data.Repository
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : Entity, new()
    {
        #region [Propriedades]
        protected readonly MeuDbContext _db;
        protected readonly DbSet<TEntity> DBSet;
        #endregion

        #region [Construtor]
        public BaseRepository(MeuDbContext db)
        {
            _db = db;
            DBSet = db.Set<TEntity>();
        }
        #endregion

        #region [Metodos]
        #region [MetodosDeAcao]
        public virtual async Task Adicionar(TEntity entity)
        {
            DBSet.Add(entity);
            await SaveChanges();
        }

        public virtual async Task AdicionarLista(IEnumerable<TEntity> listEntity)
        {
            foreach(TEntity entity in listEntity)
            {
                DBSet.Add(entity);
                await SaveChanges();
            }
        }

        public virtual async Task Alterar(TEntity entity)
        {
            DBSet.Update(entity);
            await SaveChanges();
        }

        public virtual async Task Excluir(TEntity entity)
        {
            DBSet.Remove(entity);
            await SaveChanges();
        }
        #endregion

        #region [MetodosPesquisa]
        public virtual async Task<List<TEntity>> ObterTodos()
        {
            return await DBSet.ToListAsync(); 
        }

        public virtual async Task<IEnumerable<TEntity>> Pesquisar(Expression<Func<TEntity, bool>> pesquisa)
        {
            return await DBSet.AsNoTracking().Where(pesquisa).ToListAsync();
        }

        public virtual async Task<TEntity> PesquisarId(Guid id)
        {
            return await DBSet.FirstOrDefaultAsync(x => x.Id == id);
        }

        public virtual bool ExistePorId(Guid id)
        {
            return DBSet.AsNoTracking().Any(x => x.Id == id);
        }

        public async Task<int> SaveChanges()
        {
            return await _db.SaveChangesAsync();
        }
        #endregion

        public void Dispose()
        {
            _db?.Dispose();
        }
        #endregion
    }
}
