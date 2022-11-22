namespace HospitalAPI.Dto.Profiles
{
    using AutoMapper;
    using HospitalAPI.Dto.Auth;
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.ApplicationUser;
    using HospitalLibrary.Core.Model.Blood.Enums;
    using HospitalLibrary.Core.Model.Enums;
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;

    public class PatientProfile : Profile
    {
        public PatientProfile()
        {
            //RegisterDTO to AppPatient
            CreateMap<RegisteredPatientDTO, ApplicationPatient>()
                .ForMember(
                    dest => dest.FirstName,
                    opt => opt.MapFrom(src => $"{src.ApplicationUserDTO.FirstName}")
                )
                .ForMember(
                    dest => dest.LastName,
                    opt => opt.MapFrom(src => $"{src.ApplicationUserDTO.LastName}")
                )
                .ForMember(
                    dest => dest.Email,
                    opt => opt.MapFrom(src => $"{src.ApplicationUserDTO.Email}")
                )
                .ForMember(
                   dest => dest.UserName,
                   opt => opt.MapFrom(src => $"{src.ApplicationUserDTO.Email}")
                )
                .ForMember(
                   dest => dest.DateOfBirth,
                   opt => opt.MapFrom(src => $"{src.ApplicationUserDTO.DateOfBirth}")
                )
                .ForMember(
                   dest => dest.Gender,
                   opt => opt.MapFrom(src => src.ApplicationUserDTO.Male ? $"{Gender.MALE}" : $"{Gender.FEMALE}")
                ).ForMember(
                   dest => dest.BloodType,
                   opt => opt.MapFrom(src => $"{(BloodType)System.Enum.GetValues(typeof(BloodType)).GetValue(src.BloodType)}")
                ).ForMember(
                   dest => dest.Allergies,
                   opt => opt.Ignore()
                );
        }    
    }
}
