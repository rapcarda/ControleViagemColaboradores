using Business.Interfaces.Repository;
using Business.Models;
using Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class CidadeRepository : BaseRepository<Cidade>, ICidadeRepository
    {
        public CidadeRepository(MeuDbContext db): base(db)
        {
        }

        public bool ExisteDescricao(Cidade entity)
        {
            return DBSet.AsNoTracking().Any(x => x.Descricao == entity.Descricao && x.Id != entity.Id);
        }

        public async Task<IEnumerable<Cidade>> ObterCidadesEstadosPaises()
        {
            return await DBSet.AsNoTracking().Include(x => x.Estado).ThenInclude(y => y.Pais).ToListAsync();
        }
    }
}
