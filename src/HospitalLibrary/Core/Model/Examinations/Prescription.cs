namespace HospitalLibrary.Core.Model.Examinations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using HospitalLibrary.Core.Model.Domain;
    using HospitalLibrary.Core.Model.Medicament;

    public class Prescription : Entity
    {
        public Medicament Medicament { get; set; }

        public string Description { get; set; }

        public DateRange DateRange { get; set; }

        public Prescription() { }

        public Prescription(Medicament medicament, string description, DateRange dateRange)
        {
            Medicament = medicament;
            Description = description;
            DateRange = dateRange;
        }
    }
}
