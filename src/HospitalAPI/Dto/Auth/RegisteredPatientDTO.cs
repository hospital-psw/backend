namespace HospitalAPI.Dto.Auth
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.Blood.Enums;
    using System.Collections.Generic;

    public class RegisteredPatientDTO
    {
        public BloodType BloodType { get; set; }
        public string ChoosenDoctor { get; set; }
        public List<string> Allergies { get; set; }

    }
}
