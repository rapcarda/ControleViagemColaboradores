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
        }
    }
}
