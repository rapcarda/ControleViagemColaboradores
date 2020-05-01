using Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interfaces.Repository
{
    public interface IEmpresaRepository: IBaseRepository<Empresa>
    {
        Task<IEnumerable<Empresa>> GetEmpresaComCidade();
        bool ExisteCodigo(Empresa empresa);
        bool ExisteDescricao(Empresa empresa);
    }
}
