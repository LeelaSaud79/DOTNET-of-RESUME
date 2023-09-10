
using AutoMapper;
using Resume.DTOs.EducationsDTOs;
using Resume.Models;

namespace Resume.Helpers
{
    public class EducationsProfiles: Profile
    {
        public EducationsProfiles()
        {
            CreateMap<Educations,EducationsReadDTOs >();
            CreateMap<EducationsCreateDTOs, Educations>();
            CreateMap<EducationsUpdateDTOs, Educations>();
            CreateMap<Educations, EducationsUpdateDTOs>();
        }
    }
  
}
