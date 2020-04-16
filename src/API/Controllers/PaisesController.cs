using API.ViewModel;
using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class PaisesController : MainController
    {
        private IPaisService _paisService;
        private IMapper _mapper;

        public PaisesController(IPaisService paisService,
                                IMapper mapper,
                                INotificador notificador) : base(notificador)
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

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<PaisViewModel>> GetById(Guid id)
        {
            var pais = _mapper.Map<PaisViewModel>(await _paisService.PesquisarId(id));

            if (pais == null)
                return NotFound();

            return Ok(pais);
        }

        [HttpPost]
        public async Task<ActionResult<PaisViewModel>> Post(PaisViewModel paisViewModel)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            await _paisService.Adicionar(_mapper.Map<Pais>(paisViewModel));
            return CustomResponse(paisViewModel);
        }

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

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<PaisViewModel>> Delete(Guid id)
        {
            var pais = _mapper.Map<PaisViewModel>(await _paisService.PesquisarId(id));

            if (pais == null)
                return NotFound();

            await _paisService.Excluir(id);
            return CustomResponse();
        }

    }
}
