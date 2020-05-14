using Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interfaces.Repository
{
    public interface IEmprDeptRepository: IBaseRepository<EmprDept>
    {
        Task<IEnumerable<EmprDept>> GetByDepartamento(Guid id);

        Task ExcluirRelacionamentoByDetp(Guid idDep);
    }
}
