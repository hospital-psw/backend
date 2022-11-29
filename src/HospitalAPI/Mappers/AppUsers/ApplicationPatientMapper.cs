namespace HospitalAPI.Mappers.AppUsers
{
    using HospitalAPI.Dto.AppUsers;
    using HospitalLibrary.Core.Model.ApplicationUser;
    using HospitalLibrary.Core.Model.Enums;

    public class ApplicationPatientMapper
    {
        public static ApplicationPatientDTO EntityToEntityDTO(ApplicationPatient patient) 
        {
            ApplicationPatientDTO dto = new ApplicationPatientDTO();

            dto.Id = patient.Id;
            dto.FirstName = patient.FirstName;
            dto.LastName = patient.LastName;
            dto.Email = patient.Email;
            dto.Role = Role.PATIENT.ToString();
            dto.Hospitalized = patient.Hospitalized;
            dto.BloodType = patient.BloodType;

            return dto;
        }
    }
}
