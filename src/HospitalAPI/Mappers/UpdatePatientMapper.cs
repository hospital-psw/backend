namespace HospitalAPI.Mappers
{
    using HospitalAPI.Dto;
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.Enums;

    public class UpdatePatientMapper
    {
        public static Patient EntityDtoToEntity(UpdatePatientDto dto)
        {
            Patient patient = new Patient();

            patient.Id = dto.Id;
            patient.FirstName = dto.FirstName;
            patient.LastName = dto.LastName;
            patient.Email = dto.Email;
            patient.Guest = dto.Guest;
            patient.Role = Role.PATIENT;

            return patient;
        }
    }
}