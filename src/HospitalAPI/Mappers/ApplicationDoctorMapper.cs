namespace HospitalAPI.Mappers
{
    using HospitalAPI.Dto;
    using HospitalLibrary.Core.Model.ApplicationUser;

    public class ApplicationDoctorMapper
    {
        public static ApplicationDoctorDto EntityToEntityDto(ApplicationDoctor applicationDoctor)
        {
            ApplicationDoctorDto dto = new ApplicationDoctorDto();

            dto.Id = applicationDoctor.Id;
            dto.FirstName = applicationDoctor.FirstName;
            dto.LastName = applicationDoctor.LastName;


            return dto;
        }
        public static ApplicationDoctor EntityDtoToEntity(ApplicationDoctorDto dto)
        {
            ApplicationDoctor appDoctor = new ApplicationDoctor();

            appDoctor.Id = dto.Id;
            appDoctor.FirstName = dto.FirstName;
            appDoctor.LastName = dto.LastName;


            return appDoctor;
        }
    }

}
