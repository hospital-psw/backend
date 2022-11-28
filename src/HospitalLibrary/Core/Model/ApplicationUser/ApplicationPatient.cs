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
        private object value1;
        private object value2;
        private ApplicationDoctor doc1;
        private object value3;

        public ApplicationPatient() : base() { }
        public ApplicationPatient(string firstName, string lastName, DateTime dateOfBirth, Gender gender, bool hospitalized, BloodType type)
            : base(firstName, lastName, dateOfBirth, gender)
        {
            Hospitalized = hospitalized;
            BloodType = type;
        }

        public ApplicationPatient(string firstName, string lastName, DateTime dateOfBirth, Gender gender, object value1, object value2, ApplicationDoctor doc1, object value3) : base(firstName, lastName, dateOfBirth, gender)
        {
            this.value1 = value1;
            this.value2 = value2;
            this.doc1 = doc1;
            this.value3 = value3;
        }

        public bool Hospitalized { get; set; }

        public BloodType BloodType { get; set; }
        public ApplicationDoctor applicationDoctor { get; set; }

        public List<Allergies> Allergies { get; set; }

    }
}
