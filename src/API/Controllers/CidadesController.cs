﻿using API.Extensions;
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
    public class CidadesController : MainController
    {
        private ICidadeService _cidadeService;
        private IMapper _mapper;

        public CidadesController(ICidadeService cidadeService,
                                 IMapper mapper,
                                 INotificador notificador,
                                 IUser user) : base(notificador, user)
        {
            _cidadeService = cidadeService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CidadeViewModel>>> Get()
        {
            return Ok(_mapper.Map<IEnumerable<CidadeViewModel>>(await _cidadeService.ObterCidadesEstadosPaises()));
        }

        [ClaimsAuthorize("cidade", "getId")]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CidadeViewModel>> GetById(Guid id)
        {
            var cidade = _mapper.Map<CidadeViewModel>(await _cidadeService.PesquisarId(id));

            if (cidade == null)
                return NotFound();

            return Ok(cidade);
        }

        [ClaimsAuthorize("cidade", "post")]
        [HttpPost]
        public async Task<ActionResult<CidadeViewModel>> Post(CidadeViewModel CidadeViewModel)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            await _cidadeService.Adicionar(_mapper.Map<Cidade>(CidadeViewModel));
            return CustomResponse(CidadeViewModel);
        }

        [ClaimsAuthorize("cidade", "put")]
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<CidadeViewModel>> Put(Guid id, CidadeViewModel CidadeViewModel)
        {
            if (id != CidadeViewModel.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            await _cidadeService.Alterar(_mapper.Map<Cidade>(CidadeViewModel));
            return CustomResponse(CidadeViewModel);
        }

        [ClaimsAuthorize("cidade", "delete")]
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<CidadeViewModel>> Delete(Guid id)
        {
            var estado = _mapper.Map<CidadeViewModel>(await _cidadeService.PesquisarId(id));

            if (estado == null)
                return NotFound();

            await _cidadeService.Excluir(id);
            return CustomResponse();
        }

        [AllowAnonymous]
        [HttpGet("GetCidadeCombo")]
        public async Task<ActionResult<IEnumerable<ResponseGetCombo>>> GetCombo()
        {
            var result = await _cidadeService.ObterTodos();

            return result.Select(x => new ResponseGetCombo { Codigo = string.Empty, Descricao = x.Descricao, Id = x.Id }).ToList();
        }
    }
}