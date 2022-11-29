namespace HospitalAPI.Dto
{
    using HospitalLibrary.Core.Model.ApplicationUser;
    using HospitalLibrary.Core.Model.Blood.Enums;
    using HospitalLibrary.Core.Model.Enums;
    using HospitalLibrary.Core.Model.Medicament;
    using System;
    using System.Collections.Generic;

    public class ApplicationPatientDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public BloodType BloodType { get; set; }
        public string DoctorName { get; set; }
        public List<string> Allergies { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
    }
}
