using Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interfaces.Repository
{
    public interface ICidadeRepository: IBaseRepository<Cidade>
    {
        Task<IEnumerable<Cidade>> ObterCidadesEstadosPaises();
        bool ExisteDescricao(Cidade entity);

        bool ExisteEstadoVinculado(Guid estadoId);
    }
}
