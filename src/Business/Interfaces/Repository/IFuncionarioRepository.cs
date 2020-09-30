using Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces.Repository
{
    public interface IFuncionarioRepository : IBaseRepository<Funcionario>
    {
        Task<Funcionario> GetFuncionarioComCidadeEDepto(Guid id);
        Task<IEnumerable<Funcionario>> GetFuncionarioComCidadeEDepto();
        bool ExisteCodigo(Funcionario funcionario);
        bool ExisteDescricao(Funcionario funcionario);
    }
}
