namespace HospitalLibrary.Core.Model
{
    using HospitalLibrary.Core.Model.Blood.Enums;
    using HospitalLibrary.Core.Model.Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Patient : User
    {
        public bool Hospitalized { get; set; }

        public BloodType BloodType { get; set; }

        public Patient() { }

        public Patient(bool hospitalized, List<Allergies> allergies)
        {
            Hospitalized = hospitalized;
            Allergies = allergies;
        }

        public Patient(string firstName, string lastName, string email, string password, bool hospitalized) : base(firstName, lastName, email, password, Role.PATIENT)
        {
            Hospitalized = hospitalized;
        }
    }
}
