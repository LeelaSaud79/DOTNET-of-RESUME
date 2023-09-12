using AutoMapper;
using Resume.DTOs.CertificationsDTOs;
using Resume.Models;

namespace Resume.Helpers
{
    public class CertificationsProfiles : Profile
    {
        public CertificationsProfiles()
        {
            CreateMap<Certification, CertificationsReadDTOs>();
            CreateMap<CertificationsCreateDTOs, Certification>();
            CreateMap<CertificationsUpdateDTOs, Certification>();
            CreateMap<Certification, CertificationsUpdateDTOs>();
        }
    }
}
