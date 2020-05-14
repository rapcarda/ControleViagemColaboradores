using Business.Interfaces.Repository;
using Business.Models;
using Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class EmpresaRepository : BaseRepository<Empresa>, IEmpresaRepository
    {
        public EmpresaRepository(MeuDbContext db): base(db)
        {
        }

        public bool ExisteCodigo(Empresa empresa)
        {
            return DBSet.AsNoTracking().Any(x => x.Codigo == empresa.Codigo && x.Id != empresa.Id);
        }

        public bool ExisteDescricao(Empresa empresa)
        {
            
            return DBSet.AsNoTracking().Any(x => x.Nome == empresa.Nome && x.Id != empresa.Id);
        }

        public async Task<IEnumerable<Empresa>> GetEmpresaComCidade()
        {
            return await DBSet.AsNoTracking().Include(c => c.Cidade).ToListAsync();
        }
    }
}
