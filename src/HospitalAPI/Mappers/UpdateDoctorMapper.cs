namespace HospitalAPI.Mappers
{
    using HospitalAPI.Dto;
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.Enums;

    public class UpdateDoctorMapper
    {
        public static Doctor EntityDtoToEntity(UpdateDoctorDto dto)
        {
            Doctor doctor = new Doctor();

            doctor.Id = dto.Id;
            doctor.FirstName = dto.FirstName;
            doctor.LastName = dto.LastName;
            doctor.Email = dto.Email;
            doctor.Role = Role.DOCTOR;
            doctor.Specialization = dto.Specialization;

            return doctor;
        }
    }
}
