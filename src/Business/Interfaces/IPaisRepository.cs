using Business.Models;
using System;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IPaisRepository : IBaseRepository<Pais>
    {
        Task<Pais> ObterPaisEstados(Guid id);
        bool ExisteDescricao(Pais entity);
    }
}
