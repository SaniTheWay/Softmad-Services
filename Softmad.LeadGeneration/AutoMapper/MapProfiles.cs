using AutoMapper;
using Softmad.LeadGeneration.Models.DTOs;
using Softmad.Services.Models;

namespace Softmad.LeadGeneration.AutoMapper
{
    public class MapProfiles : Profile
    {
        public MapProfiles()
        {
            CreateMap<LeadDTO, Lead>();
            CreateMap<Models.DTOs.AdditionalDetails, Visit>();
            CreateMap<Models.DTOs.CustomerDetails, Services.Models.CustomerDetails>();
            CreateMap<Models.DTOs.HospitalDetails, Services.Models.HospitalDetails>();
            CreateMap<Models.DTOs.DoctorDetails, Services.Models.DoctorDetails>();
        }
    }
}
