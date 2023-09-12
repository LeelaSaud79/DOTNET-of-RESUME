using AutoMapper;
using Resume.DTOs.ExpereincesDTOs;
using Resume.Models;

namespace Resume.Helpers
{
    public class ExperiencesProfiles : Profile
    {
        public ExperiencesProfiles()
        {
            CreateMap<Experience, ExperiencesReadDTOs>();
            CreateMap<ExperiencesCreateDTOs, Experience>();
            CreateMap<ExperiencesUpdateDTOs, Experience>();
            CreateMap<Experience, ExperiencesUpdateDTOs>();
        }
    }
}

