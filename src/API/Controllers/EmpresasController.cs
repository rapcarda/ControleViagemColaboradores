using API.Extensions;
using API.ViewModel;
using AutoMapper;
using Business.Interfaces;
using Business.Interfaces.Service;
using Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class EmpresasController : MainController
    {
        private readonly IEmpresaService _empresaService;
        private readonly IMapper _mapper;

        public EmpresasController(IEmpresaService empresaService,
                                  IMapper mapper,
                                  INotificador notificador,
                                  IUser user): base(notificador, user)
        {
            _empresaService = empresaService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmpresaViewModel>>> Get()
        {
            return Ok(_mapper.Map<IEnumerable<EmpresaViewModel>>(await _empresaService.GetEmpresaComCidade()));
        }

        [ClaimsAuthorize("empresa", "getId")]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<EmpresaViewModel>> GetById(Guid id)
        {
            var empresa = _mapper.Map<EmpresaViewModel>(await _empresaService.PesquisarId(id));

            if (empresa == null)
                return NotFound();

            return Ok(empresa);
        }

        [ClaimsAuthorize("empresa", "post")]
        [HttpPost()]
        public async Task<ActionResult<EmpresaViewModel>> Post(EmpresaViewModel empresaViewModel)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            await _empresaService.Adicionar(_mapper.Map<Empresa>(empresaViewModel));
            return CustomResponse(empresaViewModel);
        }

        [ClaimsAuthorize("empresa", "put")]
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<EmpresaViewModel>> Put(Guid id, EmpresaViewModel empresaViewModel)
        {
            if (id != empresaViewModel.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            await _empresaService.Alterar(_mapper.Map<Empresa>(empresaViewModel));
            return CustomResponse(empresaViewModel);
        }

        [ClaimsAuthorize("empresa", "delete")]
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<EmpresaViewModel>> Delete(Guid id)
        {
            var empresa = await _empresaService.PesquisarId(id);

            if (empresa == null)
                return NotFound();

            await _empresaService.Excluir(id);
            return CustomResponse();
        }
    }
}