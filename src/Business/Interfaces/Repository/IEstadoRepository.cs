using Business.Models;
using System;
using System.Threading.Tasks;

namespace Business.Interfaces.Repository
{
    public interface IEstadoRepository: IBaseRepository<Estado>
    {
        Task<Estado> ObterEstadoCidades(Guid id);
        bool ExisteCodigo(Estado entity);
        bool ExisteDescricao(Estado entity);
    }
}
