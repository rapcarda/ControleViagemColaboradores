using Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interfaces.Repository
{
    public interface IEstadoRepository: IBaseRepository<Estado>
    {
        Task<Estado> ObterEstadoCidades(Guid id);
        Task<IEnumerable<Estado>> ObterEstadosPaises();
        bool ExisteDescricao(Estado entity);
        bool ExisteEstado(Guid id);

        bool ExistePaisVinculado(Guid paisId);
    }
}
