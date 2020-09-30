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
    public class VeiculoRepository: BaseRepository<Veiculo>, IVeiculoRepositoy
    {
        public VeiculoRepository(MeuDbContext db): base(db)
        {
        }

        public bool ExisteCodigo(Veiculo veiculo)
        {
            return DBSet.AsNoTracking().Any(x => x.Codigo == veiculo.Codigo && x.Id != veiculo.Id);
        }

        public bool ExisteDescricao(Veiculo veiculo)
        {
            return DBSet.AsNoTracking().Any(x => x.Modelo == veiculo.Modelo && x.Id != veiculo.Id);
        }

        public bool ExistePlaca(Veiculo veiculo)
        {
            return DBSet.AsNoTracking().Any(x => x.Placa == veiculo.Placa && x.Id != veiculo.Id);
        }

        public async Task<Veiculo> GetVeiculoComEmpresa(Guid id)
        {
            return await DBSet.Include(e => e.Empresa).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Veiculo>> GetVeiculoComEmpresa()
        {
            return await DBSet.Include(e => e.Empresa).ToListAsync();
        }
    }
}
