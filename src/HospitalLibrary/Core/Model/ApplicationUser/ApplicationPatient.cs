namespace HospitalLibrary.Core.Model.ApplicationUser
{
    using HospitalLibrary.Core.Model.Blood.Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ApplicationPatient : ApplicationUser
    {
        public bool Hospitalized { get; set; }

        public BloodType BloodType { get; set; }
    }
}
