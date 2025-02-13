using AutoMapper;
using OnlineShop.Dtos;
using OnlineShop.Models;

namespace OnlineShop.Mappings
{
    public class ModelMapper : Profile
    {
        public ModelMapper()
        {
            CreateMap<Villa, VillaDto>();
            CreateMap<Detail, DetailDto>().ReverseMap();
        }
    }
}
