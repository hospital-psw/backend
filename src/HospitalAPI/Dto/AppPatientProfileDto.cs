namespace HospitalAPI.Dto
{
    using HospitalAPI.Dto.AppUsers;
    using HospitalLibrary.Core.Model.Enums;
    using System;
    using System.Collections.Generic;

    public class AppPatientProfileDto
    {
        public ApplicationPatientDTO applicationPatient { get; set; }
        public String ChoosenDoctor { get; set; }
        public List<string> Allergies { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
    }
}
