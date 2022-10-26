namespace HospitalAPI.Mappers
{
    using HospitalAPI.Dto;
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.Enums;

    public class NewDoctorMapper
    {
        public static Doctor EntityDtoToEntity(NewDoctorDto dto)
        {
            Doctor doctor = new Doctor();

            doctor.Specialization = dto.Specialization;
            doctor.FirstName = dto.FirstName;
            doctor.LastName = dto.LastName;
            doctor.Email = dto.Email;
            doctor.Password = dto.Password;
            doctor.Role = Role.DOCTOR;

            return doctor;
        }
    }
}
