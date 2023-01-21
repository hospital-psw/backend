namespace HospitalAPI.Mappers.AppUsers
{
    using HospitalAPI.Dto.AppUsers;
    using HospitalLibrary.Core.Model.ApplicationUser;
    using HospitalLibrary.Core.Model.Enums;

    public class ApplicationDoctorMapper
    {

        public static ApplicationDoctorDTO EntityToEntityDTO(ApplicationDoctor doctor)
        {
            ApplicationDoctorDTO dto = new ApplicationDoctorDTO();

            dto.Id = doctor.Id;
            dto.FirstName = doctor.FirstName;
            dto.LastName = doctor.LastName;
            dto.Email = doctor.Email;
            dto.Role = Role.DOCTOR.ToString();
            dto.Specialization = doctor.Specialization;
            dto.Office = RoomMapper.EntityToEntityDto(doctor.Office);
            dto.WorkingHours = WorkingHoursMapper.EntityToEntityDto(doctor.WorkHours);

            return dto;
        }

    }
}
