namespace HospitalLibrary.Core.Model.MedicalTreatment
{
    using HospitalLibrary.Core.Model.Therapy;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class MedicalTreatment : Entity
    {

        public Room Room { get; set; }

        public Patient Patient { get; set; }

        public Doctor Doctor { get; set; }

        public List<MedicamentTherapy> MedicamentTherapies { get; set; }

        public List<BloodUnitTherapy> BloodUnitTherapies { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public bool Active { get; set; }

        public string Report { get; set; }

        public MedicalTreatment()
        {

        }

        public MedicalTreatment(Room room, Patient patient, Doctor doctor, List<MedicamentTherapy> medicamentTherapies, List<BloodUnitTherapy> bloodUnitTherapies, DateTime start, DateTime end, bool active, string report)
        {
            Room = room;
            Patient = patient;
            Doctor = doctor;
            MedicamentTherapies = medicamentTherapies;
            BloodUnitTherapies = bloodUnitTherapies;
            Start = start;
            End = end;
            Active = active;
            Report = report;
        }

    }
}
