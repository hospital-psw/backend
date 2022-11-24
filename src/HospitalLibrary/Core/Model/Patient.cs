namespace HospitalLibrary.Core.Model
{
    using HospitalLibrary.Core.Model.Blood.Enums;
    using HospitalLibrary.Core.Model.Enums;
    using HospitalLibrary.Core.Model.Medicament;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Patient : User
    {
        public bool Hospitalized { get; set; }

        public List<Allergies> Allergies { get; set; }

        public BloodType BloodType { get; set; }

        public Patient() { }

        public Patient(bool hospitalized)
        {
            Hospitalized = hospitalized;
        }

        public Patient(string firstName, string lastName, string email, string password, bool hospitalized, List<Allergies> allergens) : base(firstName, lastName, email, password, Role.PATIENT)
        {
            Hospitalized = hospitalized;
            Allergies = allergens;
        }
    }
}
