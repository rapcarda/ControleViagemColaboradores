using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Extensions;
using API.ViewModel;
using API.ViewModel.Util;
using AutoMapper;
using Business.Interfaces;
using Business.Interfaces.Service;
using Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class FuncionariosController : MainController
    {
        private readonly IFuncionarioService _funcionarioService;
        private readonly IMapper _mapper;

        public FuncionariosController(IMapper mapper,
                                      IFuncionarioService funcionarioService,
                                      INotificador notificador,
                                      IUser user): base(notificador, user)
        {
            _mapper = mapper;
            _funcionarioService = funcionarioService;

        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FuncionarioViewModel>>> Get()
        {
            return Ok(_mapper.Map<IEnumerable<FuncionarioViewModel>>(await _funcionarioService.GetFuncionarioComCidadeEDepto()));
        }

        [ClaimsAuthorize("funcionario", "post")]
        [HttpPost()]
        public async Task<ActionResult<FuncionarioViewModel>> Post(FuncionarioViewModel funcViewModel)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            await _funcionarioService.Adicionar(_mapper.Map<Funcionario>(funcViewModel));
            return CustomResponse(funcViewModel);
        }

        [ClaimsAuthorize("funcionario", "put")]
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<FuncionarioViewModel>> Put(Guid id, [FromBody] FuncionarioViewModel funcViewModel)
        {
            if (id != funcViewModel.Id)
                return BadRequest();
            
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            await _funcionarioService.Alterar(_mapper.Map<Funcionario>(funcViewModel));
            return CustomResponse(funcViewModel);
        }

        [ClaimsAuthorize("funcionario", "delete")]
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<FuncionarioViewModel>> Delete(Guid id)
        {
            var func = await _funcionarioService.PesquisarId(id);

            if (func == null)
                return NotFound();

            await _funcionarioService.Excluir(id);
            return CustomResponse();
        }

        [AllowAnonymous]
        [HttpGet("GetFuncionarioaCombo")]
        public async Task<ActionResult<IEnumerable<ResponseGetCombo>>> GetCombo()
        {
            var result = await _funcionarioService.ObterTodos();

            return result.Select(x => new ResponseGetCombo { Codigo = x.Codigo, Descricao = x.Nome, Id = x.Id }).ToList();
        }
    }
}
