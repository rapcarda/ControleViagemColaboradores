using API.ViewModel;
using AutoMapper;
using Business.Models;
using System;
using System.Linq;

namespace API.Configuration
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Pais, PaisViewModel>().ReverseMap();

            CreateMap<EstadoViewModel, Estado>();
            CreateMap<Estado, EstadoViewModel>()
                .ForMember(dest => dest.NomePais, opt => opt.MapFrom(src => src.Pais.Descricao));

            CreateMap<CidadeViewModel, Cidade>();
            CreateMap<Cidade, CidadeViewModel>()
                .ForMember(dest => dest.NomeEstado, opt => opt.MapFrom(src => src.Estado.Descricao));

            CreateMap<EmpresaViewModel, Empresa>();
            CreateMap<Empresa, EmpresaViewModel>()
                .ForMember(dest => dest.NomeCidade, opt => opt.MapFrom(src => src.Cidade.Descricao));

            CreateMap<DepartamentoViewModel, Departamento>();
                //.ForMember(dest => dest.EmpresasDepartamento, opt => opt.MapFrom(src => src.EmpresasId.Select(e => new EmprDept
                // {
                //     Id = Guid.NewGuid(),
                //     EmpresaId = e,
                //     DepartamentoId = 
                // })));
            CreateMap<Departamento, DepartamentoViewModel>()
                .ForMember(dest => dest.EmpresasId, opt => opt.MapFrom(src => src.EmpresasDepartamento.Select(e => e.EmpresaId)));
        }
    }
}
