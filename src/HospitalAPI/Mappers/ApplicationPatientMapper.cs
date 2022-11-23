namespace HospitalAPI.Mappers
{
    using HospitalAPI.Dto;
    using HospitalLibrary.Core.Model.ApplicationUser;
    using HospitalLibrary.Core.Model.Blood.Enums;
    using HospitalLibrary.Core.Model.Enums;
    using HospitalLibrary.Core.Model.Medicament;
    using System.Collections.Generic;
    using System;

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
            dto.DoctorName = patient.applicationDoctor.FirstName + patient.applicationDoctor.LastName;
            dto.Email = patient.Email;

            return dto;
        }
        public static ApplicationPatient EntityDtoToEntity(ApplicationPatientDto dto)
        {
            ApplicationPatient patient = new ApplicationPatient();

            patient.Id = dto.Id;
            patient.Email = dto.Email;
            patient.FirstName = dto.FirstName;
            patient.LastName= dto.LastName;
            patient.DateOfBirth= dto.DateOfBirth;
            patient.Gender = dto.Gender;
            patient.BloodType= dto.BloodType;

            return patient;
        }
    }
}
