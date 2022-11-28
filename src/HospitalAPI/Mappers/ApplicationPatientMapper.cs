namespace HospitalAPI.Mappers
{
    using HospitalAPI.Dto;
    using HospitalLibrary.Core.Model.ApplicationUser;
    using HospitalLibrary.Core.Model.Blood.Enums;
    using HospitalLibrary.Core.Model.Enums;
    using HospitalLibrary.Core.Model.Medicament;
    using System;
    using System.Collections.Generic;

    public class ApplicationPatientMapper
    {
        public static ApplicationPatientDto EntityToEntityDto(ApplicationPatient patient)
        {
            ApplicationPatientDto dto = new ApplicationPatientDto();

            dto.Id = patient.Id;
            dto.FirstName = patient.FirstName;
            dto.LastName = patient.LastName;
            dto.DateOfBirth = patient.DateOfBirth;
            dto.Gender = patient.Gender;
            dto.BloodType = patient.BloodType;
            //dto.DoctorName = patient.applicationDoctor.FirstName + patient.applicationDoctor.LastName;
            //dto.Allergies = patient.Allergies.ConvertAll(o => o.Name);
            dto.Email = patient.Email;

            return dto;
        }
    }
}
