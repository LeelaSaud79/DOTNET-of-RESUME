using AutoMapper;
using Resume.DTOs.InfoesDTOs;
using Resume.Models;

namespace Resume.Helpers
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {


            CreateMap<Info, InfoesReadDTOs>();
            CreateMap<InfoesCreateDTOs, Info>();
            CreateMap<InfoesUpdateDTOs, Info>();
            CreateMap<Info, InfoesUpdateDTOs>();
        }
    }
}