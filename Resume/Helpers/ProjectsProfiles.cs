using AutoMapper;
using Resume.DTOs.ProjectsDTOs;
using Resume.Models;

namespace Resume.Helpers
{
    public class ProjectsProfiles : Profile
    {
        public ProjectsProfiles()
        {
           
            CreateMap<Project, ProjectsReadDTOs>();
            CreateMap<ProjectsCreateDTOs, Project>();
            CreateMap<ProjectsUpdateDTOs, Project>();
            CreateMap<Project, ProjectsUpdateDTOs>();

        }
    }
}
