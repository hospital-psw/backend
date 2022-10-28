﻿namespace IntegrationAPI.Mapping
{
    using AutoMapper;
    using IntegrationAPI.DTO;
    using IntegrationLibrary.BloodBank;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BloodBank, UpdateBloodBankDTO>();
            CreateMap<UpdateBloodBankDTO, BloodBank>();
        }
    }
}