using AutoMapper;
using Resume.DTOs.SkillsDTOs;
using Resume.Models;

namespace Resume.Helpers
{
    public class SkillsProfiles: Profile
    {
        public SkillsProfiles() 
        { 
        CreateMap<Skills, SkillsReadDTOs>();
        CreateMap<SkillsCreateDTOs, Skills>();
        CreateMap < SkillsUpdateDTOs, Skills>();
        CreateMap < Skills,SkillsCreateDTOs>();
        }
    }
   
}
