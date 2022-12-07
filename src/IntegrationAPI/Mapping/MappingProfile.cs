namespace IntegrationAPI.Mapping
{
    using AutoMapper;
    using IntegrationAPI.DTO.BloodBank;
    using IntegrationAPI.DTO.News;
    using IntegrationAPI.DTO.Notification;
    using IntegrationAPI.DTO.Tender;
    using IntegrationLibrary.BloodBank;
    using IntegrationLibrary.News;
    using IntegrationLibrary.Notification;
    using IntegrationLibrary.Tender;

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

            CreateMap<Tender, GetTenderDTO>();
            CreateMap<GetTenderDTO, Tender>();
            CreateMap<Tender, CreateTenderDTO>();
            CreateMap<CreateTenderDTO, Tender>();
            CreateMap<Tender, UpdateTenderDTO>();
            CreateMap<UpdateTenderDTO, Tender>();

            CreateMap<Notification, NotificationDTO>();
            CreateMap<NotificationDTO, Notification>();

        }
    }
}
