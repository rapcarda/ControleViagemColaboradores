using Business.Interfaces;
using Business.Models;
using Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class PaisRepository : BaseRepository<Pais>, IPaisRepository
    {
        public PaisRepository(MeuDbContext dbcontext) 
            : base(dbcontext)
        {
        }

        public async Task<Pais> ObterPaisEstados(Guid id)
        {
            return await DBSet.AsNoTracking().Include(x => x.Estados).FirstOrDefaultAsync(x => x.Id == id);
            
        }

        public bool ExisteDescricao(Pais entity)
        {
            return DBSet.AsNoTracking().Any(x => x.Descricao.ToUpper().Trim() == entity.Descricao.ToUpper().Trim() 
                    && x.Id != entity.Id);
        }
    }
}
