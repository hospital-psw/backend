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
            CreateMap<ApplicationUser, ApplicationUserDTO>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => $"{src.Id}")
                )
                .ForMember(
                    dest => dest.FirstName,
                    opt => opt.MapFrom(src => $"{src.FirstName}")
                )
                .ForMember(
                    dest => dest.LastName,
                    opt => opt.MapFrom(src => $"{src.LastName}")
                )
                .ForMember(
                    dest => dest.Email,
                    opt => opt.MapFrom(src => $"{src.Email}")
                ).ReverseMap();


            //RegisterDTO to AppUser
            CreateMap<RegisterDTO, ApplicationUser>()
                .ForMember(
                    dest => dest.FirstName,
                    opt => opt.MapFrom(src => $"{src.FirstName}")
                )
                .ForMember(
                    dest => dest.LastName,
                    opt => opt.MapFrom(src => $"{src.LastName}")
                )
                .ForMember(
                    dest => dest.Email,
                    opt => opt.MapFrom(src => $"{src.Email}")
                )
                .ForMember(
                   dest => dest.UserName,
                   opt => opt.MapFrom(src => $"{src.Email}")
                )
                .ForMember(
                   dest => dest.DateOfBirth,
                   opt => opt.MapFrom(src => $"{src.DateOfBirth}")
                )
                .ForMember(
                   dest => dest.Gender,
                   opt => opt.MapFrom(src => src.Male ? $"{Gender.MALE}" : $"{Gender.FEMALE}")
                ).ReverseMap();
        }
    }
}
