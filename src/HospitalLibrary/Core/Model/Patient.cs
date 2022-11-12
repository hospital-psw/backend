namespace HospitalLibrary.Core.Model
{
    using HospitalLibrary.Core.Model.Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Patient : User
    {
        public bool Hospitalized { get; set; }

        public Patient() { }

        public Patient(bool hospitalized)
        {
            Hospitalized = hospitalized;
        }

        public Patient(string firstName, string lastName, string email, string password, bool guest) : base(firstName, lastName, email, password, Role.PATIENT)
        {
            Guest = guest;
        }
    }
}
