namespace HospitalAPI.Dto.Profiles
{
    using AutoMapper;
    using HospitalAPI.Dto.Auth;
    using HospitalLibrary.Core.Model.ApplicationUser;

    public class LoginProfile : Profile
    {
        public LoginProfile()
        {
            //AppUser to LoginResponse
            CreateMap<ApplicationUser, LoginResponseDTO>()
               .ForMember(
                   dest => dest.Id,
                   opt => opt.MapFrom(src => $"{src.Id}")
               )
               .ForMember(
                   dest => dest.Email,
                   opt => opt.MapFrom(src => $"{src.Email}")
               ).ReverseMap();
        }
    }
}
