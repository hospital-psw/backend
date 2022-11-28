namespace HospitalLibrary.Core.Model.ApplicationUser
{
    using HospitalLibrary.Core.Model.Blood.Enums;
    using HospitalLibrary.Core.Model.Enums;
    using HospitalLibrary.Core.Model.Medicament;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ApplicationPatient : ApplicationUser
    {
        public bool Hospitalized { get; set; }
        public BloodType BloodType { get; set; }
        public ApplicationDoctor applicationDoctor { get; set; }
        public List<Allergies> Allergies { get; set; }

        public ApplicationPatient() : base() { }
        public ApplicationPatient(string firstName, string lastName, DateTime dateOfBirth, Gender gender, bool hospitalized, BloodType type)
            : base(firstName, lastName, dateOfBirth, gender)
        {
            Hospitalized = hospitalized;
            BloodType = type;
        }

    }
}
