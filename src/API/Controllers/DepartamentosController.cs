using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Extensions;
using API.ViewModel;
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
    public class DepartamentosController : MainController
    {
        private readonly IDepartamentoService _departamentoService;
        private readonly IMapper _mapper;

        public DepartamentosController(IDepartamentoService departamentoService,
                                       IMapper mapper,
                                       INotificador notificador,
                                       IUser user): base(notificador, user)
        {
            _departamentoService = departamentoService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepartamentoViewModel>>> Get()
        {
            return Ok(_mapper.Map<IEnumerable<DepartamentoViewModel>>(await _departamentoService.ObterTodos()));
        }

        [ClaimsAuthorize("departamento", "getId")]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<DepartamentoViewModel>> GetById(Guid id)
        {
            var depto = _mapper.Map<DepartamentoViewModel>(await _departamentoService.PesquisarId(id));

            if (depto == null)
                return NotFound();

            return Ok(depto);
        }

        [ClaimsAuthorize("departamento", "post")]
        [HttpPost]
        public async Task<ActionResult<DepartamentoViewModel>> Post(DepartamentoViewModel deptoViewModel)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            var depto = _mapper.Map<Departamento>(deptoViewModel);
            depto.EmpresasDepartamento = CarregarRelacionamento(deptoViewModel).ToList();

            await _departamentoService.Adicionar(depto);
            return CustomResponse(deptoViewModel);
        }

        [ClaimsAuthorize("departamento", "put")]
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<DepartamentoViewModel>> Put(Guid id, DepartamentoViewModel deptoViewModel)
        {
            if (id != deptoViewModel.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            var depto = _mapper.Map<Departamento>(deptoViewModel);
            depto.EmpresasDepartamento = CarregarRelacionamento(deptoViewModel).ToList();

            await _departamentoService.Alterar(depto);
            return CustomResponse(deptoViewModel);
        }

        [ClaimsAuthorize("departamento", "delete")]
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<DepartamentoViewModel>> Delete(Guid id)
        {
            var depto = await _departamentoService.PesquisarId(id);

            if (depto == null)
                return NotFound();

            await _departamentoService.Excluir(id);
            return CustomResponse();
        }

        private IEnumerable<EmprDept> CarregarRelacionamento(DepartamentoViewModel deptoViewModel)
        {
            return deptoViewModel.EmpresasId.Select(x => new EmprDept
            {
                Id = Guid.NewGuid(),
                EmpresaId = x,
                DepartamentoId = deptoViewModel.Id
            });
        }
    }
}