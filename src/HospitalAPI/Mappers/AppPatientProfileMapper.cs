namespace HospitalAPI.Mappers
{
    using HospitalAPI.Dto;
    using HospitalAPI.Dto.AppUsers;
    using HospitalAPI.Mappers.AppUsers;
    using HospitalLibrary.Core.Model.ApplicationUser;
    using HospitalLibrary.Core.Model.Enums;
    using System.Linq;

    public class AppPatientProfileMapper
    {
        public static AppPatientProfileDto EntityToEntityDTO(ApplicationPatient patient)
        {
            AppPatientProfileDto dto = new AppPatientProfileDto();

            dto.applicationPatient = ApplicationPatientMapper.EntityToEntityDTO(patient);
            dto.ChoosenDoctor = patient.applicationDoctor.FirstName + " " + patient.applicationDoctor.LastName;
            dto.Allergies = patient.Allergies.Select(a => a.Name).ToList();
            dto.BirthDate = patient.DateOfBirth;
            dto.Gender = patient.Gender;

            return dto;
        }
    }
}
