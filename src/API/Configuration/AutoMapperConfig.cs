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
        }
    }
}
