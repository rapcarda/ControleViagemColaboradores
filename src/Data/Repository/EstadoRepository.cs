using Business.Interfaces.Repository;
using Business.Models;
using Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class EstadoRepository : BaseRepository<Estado>, IEstadoRepository
    {
        public EstadoRepository(MeuDbContext db): base(db)
        {
        }

        public bool ExisteDescricao(Estado entity)
        {
            return DBSet.AsNoTracking().Any(x => x.Descricao.ToUpper().Trim() == entity.Descricao.ToUpper().Trim()
                && x.Id != entity.Id);
        }

        public bool ExisteEstado(Guid id)
        {
            return DBSet.AsNoTracking().Any(x => x.Id == id);
        }

        public async Task<Estado> ObterEstadoCidades(Guid id)
        {
            return await DBSet.AsNoTracking().Include(x => x.Cidades).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Estado>> ObterEstadosPaises()
        {
            return await DBSet.AsNoTracking().Include(x => x.Pais).ToListAsync();
        }
    }
}
