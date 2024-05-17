using AutoMapper;
using BaseApi.Data;
using BaseApi.DTOs;

namespace BaseApi.Configurations
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Define mappings between entities and DTOs
            CreateMap<Brand, BrandDto>().ReverseMap();
            CreateMap<Status, StatusDto>().ReverseMap();
        }
    }
}
