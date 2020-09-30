using Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interfaces.Repository
{
    public interface IVeiculoRepositoy: IBaseRepository<Veiculo>
    {
        Task<Veiculo> GetVeiculoComEmpresa(Guid id);
        Task<IEnumerable<Veiculo>> GetVeiculoComEmpresa();
        bool ExisteCodigo(Veiculo veiculo);
        bool ExisteDescricao(Veiculo veiculo);
        bool ExistePlaca(Veiculo veiculo);
    }
}
