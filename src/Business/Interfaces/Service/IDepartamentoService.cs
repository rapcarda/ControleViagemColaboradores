using Business.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Business.Interfaces.Service
{
    public interface IDepartamentoService: IBaseService<Departamento>
    {
        Task Adicionar(Departamento entity);
        Task Alterar(Departamento entity);
        Task Excluir(Guid id);

        //Task<IEnumerable<Departamento>> GetEmpresaComCidade();
        Task<Departamento> PesquisarId(Guid id);
        Task<IEnumerable<Departamento>> ObterTodos();
        Task<IEnumerable<Departamento>> Pesquisar(Expression<Func<Departamento, bool>> pesquisa);
        bool ExisteDescricao(Departamento empresa);
    }
}
