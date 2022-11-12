namespace HospitalAPI.Mappers
{
    using HospitalAPI.Dto;
    using HospitalLibrary.Core.Model;

    public class PatientMapper
    {
        public static PatientDto EntityToEntityDto(Patient patient)
        {
            PatientDto dto = new PatientDto();

            dto.FirstName = patient.FirstName;
            dto.LastName = patient.LastName;
            dto.Email = patient.Email;
            dto.Id = patient.Id;
            dto.Hospitalized = patient.Hospitalized;
            dto.Role = patient.Role;

            return dto;
        }
    }
}
