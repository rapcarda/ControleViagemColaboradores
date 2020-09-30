using API.Extensions;
using API.ViewModel;
using API.ViewModel.Util;
using AutoMapper;
using Business.Interfaces;
using Business.Interfaces.Service;
using Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class VeiculosController : MainController
    {
        private readonly IVeiculoService _veiculoService;
        private readonly IMapper _mapper;

        public VeiculosController(IVeiculoService veiculoService,
                                 IMapper mapper,
                                 INotificador notificador, 
                                 IUser user): base(notificador, user)
        {
            _veiculoService = veiculoService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VeiculoViewModel>>> Get()
        {
            return Ok(_mapper.Map<IEnumerable<VeiculoViewModel>>(await _veiculoService.GetVeiculoComEmpresa()));
        }

        [ClaimsAuthorize("veiculo", "post")]
        [HttpPost()]
        public async Task<ActionResult<VeiculoViewModel>> Post(VeiculoViewModel veiculoViewModel)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            await _veiculoService.Adicionar(_mapper.Map<Veiculo>(veiculoViewModel));
            return CustomResponse(veiculoViewModel);
        }

        [ClaimsAuthorize("veiculo", "put")]
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<VeiculoViewModel>> Put(Guid id, [FromBody] VeiculoViewModel veiculoViewModel)
        {
            if (id != veiculoViewModel.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            await _veiculoService.Alterar(_mapper.Map<Veiculo>(veiculoViewModel));
            return CustomResponse(veiculoViewModel);
        }

        [ClaimsAuthorize("veiculo", "delete")]
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<VeiculoViewModel>> Delete(Guid id)
        {
            var veiculo = await _veiculoService.PesquisarId(id);

            if (veiculo == null)
                return NotFound();

            await _veiculoService.Excluir(id);
            return CustomResponse();
        }

        [AllowAnonymous]
        [HttpGet("GetVeiculoCombo")]
        public async Task<ActionResult<IEnumerable<ResponseGetCombo>>> GetCombo()
        {
            var result = await _veiculoService.ObterTodos();

            return result.Select(x => new ResponseGetCombo { Codigo = x.Codigo, Descricao = x.Placa, Id = x.Id }).ToList();
        }
    }
}
