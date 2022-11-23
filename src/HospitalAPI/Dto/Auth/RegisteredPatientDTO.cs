namespace HospitalAPI.Dto.Auth
{
    using HospitalLibrary.Core.Model;
    using System.Collections.Generic;

    public class RegisteredPatientDTO
    {
        public RegisterDTO ApplicationUserDTO { get; set; }
        public int BloodType { get; set; }
        public int ChoosenDoctor { get; set; }
        public List<int> Allergies { get; set; }

    }
}
