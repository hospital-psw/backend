﻿namespace HospitalLibrary.Core.Model.Examinations
{
    using HospitalLibrary.Core.Model.Domain;
    using HospitalLibrary.Core.Model.Medicament;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

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

        public override bool Equals(object obj)
        {
            var prescription = obj as Prescription;

            if (prescription == null)
            {
                return false;
            }

            return this.Id.Equals(prescription.Id);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}
