namespace HospitalLibrary.Core.Model.Examinations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Anamnesis : Entity
    {
        public Appointment Appointment { get; set; }

        public string Description { get; set; }

        public List<Symptom> Symptoms { get; set; }

        public List<Prescription> Prescriptions { get; set; }

        public Anamnesis() { }

        public Anamnesis(Appointment appointment, string description)
        {
            Appointment = appointment;
            Description = description;
        }

        public override bool Equals(object obj)
        {
            var anamnesis = obj as Anamnesis;

            if (anamnesis == null)
            {
                return false;
            }

            return this.Id.Equals(anamnesis.Id);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}
