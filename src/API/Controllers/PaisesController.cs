using API.Extensions;
using API.ViewModel;
using API.ViewModel.Util;
using AutoMapper;
using Business.Interfaces;
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
    public class PaisesController : MainController
    {
        private IPaisService _paisService;
        private IMapper _mapper;

        public PaisesController(IPaisService paisService,
                                IMapper mapper,
                                INotificador notificador,
                                IUser user) : base(notificador, user)
        {
            _paisService = paisService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaisViewModel>>> Get()
        {
            return Ok(_mapper.Map<IEnumerable<PaisViewModel>>(await _paisService.ObterTodos()));
        }

        [ClaimsAuthorize("pais", "getId")]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<PaisViewModel>> GetById(Guid id)
        {
            var pais = _mapper.Map<PaisViewModel>(await _paisService.PesquisarId(id));

            if (pais == null)
                return NotFound();

            return Ok(pais);
        }

        [ClaimsAuthorize("pais", "post")]
        [HttpPost]
        public async Task<ActionResult<PaisViewModel>> Post(PaisViewModel paisViewModel)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            await _paisService.Adicionar(_mapper.Map<Pais>(paisViewModel));
            return CustomResponse(paisViewModel);
        }

        [ClaimsAuthorize("pais", "put")]
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<PaisViewModel>> Put(Guid id, PaisViewModel paisViewModel)
        {
            if (id != paisViewModel.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            await _paisService.Alterar(_mapper.Map<Pais>(paisViewModel));
            return CustomResponse(paisViewModel);
        }

        [ClaimsAuthorize("pais", "delete")]
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<PaisViewModel>> Delete(Guid id)
        {
            var pais = await _paisService.PesquisarId(id);

            if (pais == null)
                return NotFound();
 
            var paisModel = _mapper.Map<PaisViewModel>(pais);

            await _paisService.Excluir(id);

            return CustomResponse();
        }

        [AllowAnonymous]
        [HttpGet("GetPaisCombo")]
        public async Task<ActionResult<IEnumerable<ResponseGetCombo>>> GetCombo()
        {
            var result = await _paisService.ObterTodos();

            return result.Select(x => new ResponseGetCombo { Codigo = string.Empty, Descricao = x.Descricao, Id = x.Id }).ToList();
        }

    }
}
