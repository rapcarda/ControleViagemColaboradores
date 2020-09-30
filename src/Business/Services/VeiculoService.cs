using Business.Interfaces;
using Business.Interfaces.Repository;
using Business.Interfaces.Service;
using Business.Models;
using Business.Validation;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Business.Services
{
    public class VeiculoService: BaseService<Veiculo>, IVeiculoService
    {
        private readonly IVeiculoRepositoy _veiculoRepository;
        private readonly IEmpresaRepository _empresaRepository;

        public VeiculoService(IVeiculoRepositoy veiculoRepository,
                              IEmpresaRepository empresaRepository,
                              INotificador notificador): base(notificador)
        {
            _veiculoRepository = veiculoRepository;
            _empresaRepository = empresaRepository;
        }

        public async Task Adicionar(Veiculo entity)
        {
            if (!IsValid(entity))
                return;

            await _veiculoRepository.Adicionar(entity);
        }

        public async Task Alterar(Veiculo entity)
        {
            if (!IsValid(entity))
                return;

            await _veiculoRepository.Alterar(entity);
        }

        public async Task Excluir(Guid id)
        {
            var veiculo = await _veiculoRepository.PesquisarId(id);
            await _veiculoRepository.Excluir(veiculo);
        }

        public bool ExisteCodigo(Veiculo veiculo)
        {
            return _veiculoRepository.ExisteCodigo(veiculo);
        }

        public bool ExisteDescricao(Veiculo veiculo)
        {
            return _veiculoRepository.ExisteDescricao(veiculo);
        }

        public bool ExistePlaca(Veiculo veiculo)
        {
            return _veiculoRepository.ExistePlaca(veiculo);
        }

        public async Task<IEnumerable<Veiculo>> GetVeiculoComEmpresa()
        {
            return await _veiculoRepository.GetVeiculoComEmpresa();
        }

        public async Task<IEnumerable<Veiculo>> ObterTodos()
        {
            return await _veiculoRepository.ObterTodos();
        }

        public async Task<IEnumerable<Veiculo>> Pesquisar(Expression<Func<Veiculo, bool>> pesquisa)
        {
            return await _veiculoRepository.Pesquisar(pesquisa);
        }

        public async Task<Veiculo> PesquisarId(Guid id)
        {
            return await _veiculoRepository.PesquisarId(id);
        }

        public bool ExisteEmpresa(Veiculo veiculo)
        {
            return _empresaRepository.ExistePorId(veiculo.EmpresaId);
        }

        private bool IsValid(Veiculo entity)
        {
            if (!ExecutarValidacao(new VeiculoValidation(), entity))
                return false;

            if (ExisteCodigo(entity))
            {
                Notificar("Já existe um veículo com o mesmo código");
                return false;
            }

            if (ExisteDescricao(entity))
            {
                Notificar("Já existe um veículo com o mesmo nome");
                return false;
            }

            if (ExistePlaca(entity))
            {
                Notificar("Já existe um veículo com a mesma placa");
                return false;
            }

            if (!ExisteEmpresa(entity))
            {
                Notificar("Empresa não cadastrada");
                return false;
            }

            return true;
        }
    }
}
