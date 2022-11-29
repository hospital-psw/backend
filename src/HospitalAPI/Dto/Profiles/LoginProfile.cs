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
            CreateMap<ApplicationUser, LoginResponseDTO>();
        }
    }
}
