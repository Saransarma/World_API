using AutoMapper;
using World_Api.DTO.Country;
using World_Api.Models;

namespace World_Api.Common
{
    public class MappingProfile:Profile 
    {
        public MappingProfile()
        {
            //Source ,Destination
            CreateMap<CreateCountryDTO, Country>();
            //CreateMap<Country, CreateCountryDTO>().ReverseMap();
        }
    }
}
