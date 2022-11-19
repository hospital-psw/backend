namespace HospitalLibrary.Core.Model
{
    using HospitalLibrary.Core.Model.Blood.Enums;
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

        public Patient(bool hospitalized)
        {
            Hospitalized = hospitalized;
        }
    }
}
