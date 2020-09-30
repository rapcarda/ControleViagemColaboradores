using Business.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces.Service
{
    public interface IVeiculoService: IBaseService<Veiculo>
    {
        Task Adicionar(Veiculo entity);
        Task Alterar(Veiculo entity);
        Task Excluir(Guid id);

        Task<IEnumerable<Veiculo>> GetVeiculoComEmpresa();
        Task<Veiculo> PesquisarId(Guid id);
        Task<IEnumerable<Veiculo>> Pesquisar(Expression<Func<Veiculo, bool>> pesquisa);
        Task<IEnumerable<Veiculo>> ObterTodos();

        bool ExisteCodigo(Veiculo veiculo);
        bool ExisteDescricao(Veiculo veiculo);
        bool ExistePlaca(Veiculo veiculo);
    }
}
