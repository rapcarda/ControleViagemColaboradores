using Business.Interfaces.Repository;
using Business.Models;
using Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class DepartamentoRepository: BaseRepository<Departamento>, IDepartamentoRepository
    {
        public DepartamentoRepository(MeuDbContext db): base(db)
        {
        }

        public bool ExisteCodigo(Departamento dpto)
        {
            return DBSet.AsNoTracking().Any(x => x.Codigo == dpto.Codigo && x.Id != dpto.Id);
        }

        public bool ExisteDescricao(Departamento dpto)
        {
            return DBSet.AsNoTracking().Any(x => x.Nome == dpto.Nome && x.Id != dpto.Id);
        }

        public async Task<Departamento> GetDepartamentoComEmpresa(Guid id)
        {
            return await DBSet.Include(r => r.EmpresasDepartamento).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Departamento>> GetDepartamentoComEmpresa()
        {
            return await DBSet.AsNoTracking().Include(r => r.EmpresasDepartamento).ToListAsync();
        }

        public bool ExisteEmpresaVinculado(Guid empresaId)
        {
            return DBSet.AsNoTracking().Any(x => x.EmpresasDepartamento.Any(y => y.EmpresaId == empresaId));
        }
    }
}
