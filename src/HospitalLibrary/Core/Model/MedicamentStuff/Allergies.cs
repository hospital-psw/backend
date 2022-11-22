﻿namespace HospitalLibrary.Core.Model.Medicament
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Allergies : Entity
    {
        public string Name { get; set; }
        public List<Medicament> Medicaments { get; set; }
        public List<Patient> Patients { get; set; }

        public Allergies(string name)
        {
            Name = name;
            Medicaments = new List<Medicament>();
            Patients = new List<Patient>();
        }

        public Allergies()
        {
            Medicaments = new List<Medicament>();
            Patients = new List<Patient>();
        }
    }
}
