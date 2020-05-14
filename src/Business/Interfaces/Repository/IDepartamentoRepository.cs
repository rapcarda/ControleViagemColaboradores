using Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interfaces.Repository
{
    public interface IDepartamentoRepository: IBaseRepository<Departamento>
    {
        Task<Departamento> GetDepartamentoComEmpresa(Guid id);
        Task<IEnumerable<Departamento>> GetDepartamentoComEmpresa();
        bool ExisteCodigo(Departamento dpto);
        bool ExisteDescricao(Departamento dpto);
    }
}
