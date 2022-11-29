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

            CreateMap<ApplicationDoctor, ApplicationDoctorDTO>()
                .ForMember(
                  dest => dest.Role,
                  opt => opt.MapFrom(src => $"{"Doctor"}")
                );
        }
    }
}
