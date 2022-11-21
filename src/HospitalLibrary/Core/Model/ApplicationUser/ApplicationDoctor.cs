namespace HospitalLibrary.Core.Model.ApplicationUser
{
    using HospitalLibrary.Core.Model.Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ApplicationDoctor : ApplicationUser
    {
        public Specialization Specialization { get; set; }

        public WorkingHours WorkHours { get; set; }

        public Room Office { get; set; }
    }
}
