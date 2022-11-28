namespace HospitalAPI.Dto.Profiles
{
    using AutoMapper;
    using HospitalAPI.Dto.AppUsers;
    using HospitalLibrary.Core.Model.ApplicationUser;
    using HospitalLibrary.Core.Model.Blood.Enums;
    using HospitalLibrary.Core.Model.Enums;

    public class ApplicationDoctorProfile : Profile
    {
        public ApplicationDoctorProfile()
        {
            //EntityToEntityDTO
            CreateMap<ApplicationDoctor, ApplicationDoctorDTO>()
               .ForMember(
                   dest => dest.Id,
                   opt => opt.MapFrom(src => $"{src.Id}")
                ).ForMember(
                   dest => dest.FirstName,
                   opt => opt.MapFrom(src => $"{src.FirstName}")
                ).ForMember(
                   dest => dest.LastName,
                   opt => opt.MapFrom(src => $"{src.LastName}")
                ).ForMember(
                   dest => dest.Email,
                   opt => opt.MapFrom(src => $"{src.Email}")
                ).ForMember(
                  dest => dest.Specialization,
                  opt => opt.MapFrom(src => $"{((Specialization)src.Specialization)}")
                ).ForMember(
                  dest => dest.Role,
                  opt => opt.MapFrom(src => $"{"Doctor"}")
                );
        }
    }
}
