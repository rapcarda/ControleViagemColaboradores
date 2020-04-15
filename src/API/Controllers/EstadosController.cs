using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.ViewModel;
using AutoMapper;
using Business.Interfaces;
using Business.Interfaces.Service;
using Business.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class EstadosController : MainController
    {
        private IEstadoService _estadoService;
        private IMapper _mapper;

        public EstadosController(IEstadoService estadoService,
                                 IMapper mapper, 
                                 INotificador notificador) : base(notificador)
        {
            _estadoService = estadoService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EstadoViewModel>>> Get()
        {
            return Ok(_mapper.Map<IEnumerable<EstadoViewModel>>(await _estadoService.ObterEstadosPaises()));
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<EstadoViewModel>> GetById(Guid id)
        {
            var estado = _mapper.Map<EstadoViewModel>(await _estadoService.PesquisarId(id));

            if (estado == null)
                return NotFound();

            return Ok(estado);
        }

        [HttpPost]
        public async Task<ActionResult<EstadoViewModel>> Post(EstadoViewModel estadoViewModel)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            await _estadoService.Adicionar(_mapper.Map<Estado>(estadoViewModel));
            return CustomResponse(estadoViewModel);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<EstadoViewModel>> Put(Guid id, EstadoViewModel estadoViewModel)
        {
            if (id != estadoViewModel.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            await _estadoService.Alterar(_mapper.Map<Estado>(estadoViewModel));
            return CustomResponse(estadoViewModel);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<EstadoViewModel>> Delete(Guid id)
        {
            var estado = _mapper.Map<EstadoViewModel>(await _estadoService.PesquisarId(id));

            if (estado == null)
                return NotFound();

            await _estadoService.Excluir(id);
            return CustomResponse();
        }
    }
}