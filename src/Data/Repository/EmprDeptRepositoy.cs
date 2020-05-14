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
    public class EmprDeptRepositoy: BaseRepository<EmprDept>, IEmprDeptRepository
    {
        public EmprDeptRepositoy(MeuDbContext context): base(context)
        {
        }

        public async Task<IEnumerable<EmprDept>> GetByDepartamento(Guid id)
        {
            return await DBSet.Where(x => x.DepartamentoId == id).ToListAsync();
        }

        public async Task ExcluirRelacionamentoByDetp(Guid idDep)
        {
            var relac = await GetByDepartamento(idDep);

            if (relac != null)
            {
                foreach(EmprDept emprDepr in relac)
                {
                    await Excluir(emprDepr);
                }
            }
        }
    }
}
