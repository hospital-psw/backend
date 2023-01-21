namespace HospitalAPI.Dto.Profiles
{
    using AutoMapper;
    using HospitalAPI.Dto.AppUsers;
    using HospitalAPI.Dto.Auth;
    using HospitalLibrary.Core.Model.ApplicationUser;
    using HospitalLibrary.Core.Model.Enums;
    using System.Data;

    public class ApplicationUserProfile : Profile
    {
        public ApplicationUserProfile()
        {
            //AppUser to AppUserDTO
            CreateMap<ApplicationUser, ApplicationUserDTO>();

            //RegisterDTO to AppUser
            CreateMap<RegisterDTO, ApplicationUser>()
                .ForMember(
                   dest => dest.Gender,
                   opt => opt.MapFrom(src => src.Male ? $"{Gender.MALE}" : $"{Gender.FEMALE}")
                ).ForMember(
                    dest => dest.UserName,
                    opt => opt.MapFrom(src => $"{src.Email}")
                );
        }
    }
}
