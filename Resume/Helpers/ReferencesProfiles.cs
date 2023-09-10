using AutoMapper;
using Resume.DTOs.ReferencesDTOs;
using Resume.Models;

namespace Resume.Helpers
{
    public class ReferencesProfiles : Profile
    {
        public ReferencesProfiles()
        {
            CreateMap<References, ReferencesReadDTOs>();
            CreateMap<ReferencesCreateDTOs, References>();
            CreateMap<ReferencesUpdateDTOs, References>();
            CreateMap<References, ReferencesUpdateDTOs>();
        }
    }
}
