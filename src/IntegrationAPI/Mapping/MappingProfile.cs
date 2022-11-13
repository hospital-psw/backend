namespace IntegrationAPI.Mapping
{
    using AutoMapper;
    using IntegrationAPI.DTO.BloodBank;
    using IntegrationAPI.DTO.News;
    using IntegrationLibrary.BloodBank;
    using IntegrationLibrary.News;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BloodBank, UpdateBloodBankDTO>();
            CreateMap<UpdateBloodBankDTO, BloodBank>();
            CreateMap<BloodBank, GetBloodBankDTO>();
            CreateMap<RegisterBloodBankDTO, BloodBank>();

            CreateMap<ManagerNewsDTO, News>();
            CreateMap<UserNewsDTO, News>();
            CreateMap<News, ManagerNewsDTO>();
            CreateMap<News, UserNewsDTO>();
            
        }
    }
}
