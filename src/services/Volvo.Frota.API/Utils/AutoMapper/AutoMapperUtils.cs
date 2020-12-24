using AutoMapper;
using Volvo.Frota.API.Dtos;
using Volvo.Frota.API.Dtos.Pagination;
using Volvo.Frota.API.Models;
using Volvo.Frota.API.Utils.Pagination;

namespace Volvo.Frota.API.Utils.AutoMapper
{
    public class AutoMapperUtils
    {
        public static IConfigurationProvider GetConfigurationMappings()
        {
            return new MapperConfiguration(cfg => 
            {
                cfg.CreateMap<NewCaminhaoDto, Caminhao>().ReverseMap();
                cfg.CreateMap<UpdateCaminhaoDto, Caminhao>().ReverseMap();
                cfg.CreateMap<CaminhaoDto, Caminhao>().ReverseMap();

                cfg.CreateMap<PaginationFilter, PaginationFilterDto>().ReverseMap();
            });
        }  
    }
}
