using API.ViewModel;
using AutoMapper;
using Business.Models;

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
        }
    }
}
