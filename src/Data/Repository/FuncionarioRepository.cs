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
    public class FuncionarioRepository: BaseRepository<Funcionario>, IFuncionarioRepository
    {
        public FuncionarioRepository(MeuDbContext db): base(db)
        {
        }
        
        public bool ExisteCodigo(Funcionario funcionario)
        {
            return DBSet.AsNoTracking().Any(x => x.Codigo == funcionario.Codigo && x.Id != funcionario.Id);
        }

        public bool ExisteDescricao(Funcionario funcionario)
        {
            return DBSet.AsNoTracking().Any(x => x.Nome == funcionario.Nome && x.Id != funcionario.Id);
        }

        public async Task<Funcionario> GetFuncionarioComCidadeEDepto(Guid id)
        {
            return await DBSet.Include(c => c.Cidade).Include(d => d.Departamento).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Funcionario>>GetFuncionarioComCidadeEDepto()
        {
            return await DBSet.Include(c => c.Cidade).Include(d => d.Departamento).ToListAsync();
        }
    }
}
