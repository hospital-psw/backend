namespace IntegrationAPI.Mapping
{
    using AutoMapper;
    using IntegrationAPI.DTO.BloodBank;
    using IntegrationLibrary.BloodBank;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BloodBank, UpdateBloodBankDTO>();
            CreateMap<UpdateBloodBankDTO, BloodBank>();
            CreateMap<BloodBank, GetBloodBankDTO>();
            CreateMap<RegisterBloodBankDTO, BloodBank>();
        }
    }
}
