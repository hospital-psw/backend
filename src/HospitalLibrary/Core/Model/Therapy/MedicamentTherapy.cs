namespace HospitalLibrary.Core.Model.Therapy
{
    using HospitalLibrary.Core.Model.Medicament;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class MedicamentTherapy : Therapy
    {

        public Medicament Medicament { get; set; }

        public int AmountOfMedicament { get; set; }

        public MedicamentTherapy()
        {

        }

        public MedicamentTherapy(Medicament medicament, int amount, DateTime start, DateTime end, string about)
        {
            Medicament = medicament;
            AmountOfMedicament = amount;
            Start = start;
            End = end;
            About = about;
        }

    }
}
