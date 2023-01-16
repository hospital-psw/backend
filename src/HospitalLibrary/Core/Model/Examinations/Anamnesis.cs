namespace HospitalLibrary.Core.Model.Examinations
{
    using HospitalLibrary.Core.Infrastucture;
    using HospitalLibrary.Core.Model.Enums;
    using HospitalLibrary.Core.Model.Events;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Anamnesis : EventSourcedAggregate
    {
        public Appointment Appointment { get; set; }

        public string Description { get; set; }

        public List<Symptom> Symptoms { get; set; }

        public List<Prescription> Prescriptions { get; set; }

        public Anamnesis() { }

        private void InstantiateData(Appointment appointment)
        {
            Appointment = appointment;
            List<Symptom> symptoms = new List<Symptom>();
            List<Prescription> prescriptions = new List<Prescription>();
        }

        private Anamnesis(Appointment appointment, string description)
        {
            InstantiateData(appointment);
            Description = description;
        }

        private Anamnesis(Appointment appointment)
        {
            InstantiateData(appointment);
        }

        public static Anamnesis Create(Appointment appointment)
        {
            Anamnesis anamnesis = new Anamnesis(appointment); ;
            return anamnesis;
        }

        public static Anamnesis Create(Appointment appointment, string description)
        {
            Anamnesis anamnesis = new Anamnesis(appointment, description);
            return anamnesis;
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

        public void ChangeSymptoms(SymptomsChanged evt, Symptom symptom)
        {
            if (evt.Status == SymptomEventStatus.ADDED)
            {
                Symptom symy = Symptoms.Find(s => s.Id == evt.SymptomId);
                if (symy != null) return;
                Symptoms.Add(symptom);
            }
            Causes(evt);
        }

        public void AddDescription(DescriptionCreated evt)
        {
            Causes(evt);
        }

        public void AddPrescription(PrescriptionCreated evt, Prescription presc)
        {
            if (Prescriptions.Find(p => p.Medicament.Id == presc.Medicament.Id) != null) return;
            Prescriptions.Add(presc);
            Causes(evt);
        }

        public void RemovePrescription(PrescriptionRemoved evt)
        {
            Causes(evt);
        }

        public void FinishExamination(ExaminationFinished evt)
        {
            Causes(evt);
        }

        public void StartExamination(ExaminationStarted evt)
        {
            Causes(evt);
        }

        public void ExecuteEvent(ExaminationEvent evt)
        {
            Causes(evt);
        }

        private void Causes(DomainEvent @event)
        {
            Changes.Add(@event);
            Apply(@event);
        }

        public override void Apply(DomainEvent changes)
        {
            When((dynamic)changes);
            Version += 1;
        }

        private void When(SymptomsChanged symptomsChanged)
        {
            if (symptomsChanged.Status == SymptomEventStatus.REMOVED)
            {
                Symptom symptom = Symptoms.Find(s => s.Id == symptomsChanged.SymptomId);
                if (symptom == null) return;
                Symptoms.Remove(symptom);
            }
        }

        private void When(DescriptionCreated descriptionCreated)
        {
            Description = descriptionCreated.Description;
        }

        private void When(PrescriptionCreated prescriptionCreated)
        {
            return;
        }

        private void When(PrescriptionRemoved prescriptionRemoved)
        {
            Prescription removePrescription = Prescriptions.Find(p => p.Id == prescriptionRemoved.PrescriptionId);
            if (removePrescription == null) return;
            Prescriptions.Remove(removePrescription);
        }

        private void When(ExaminationFinished examinationFinished)
        {
            Appointment.IsDone = true;
        }

        private void When(ExaminationStarted examinationStarted)
        {
            return;
        }

        private void When(ExaminationEvent examinationEvent)
        {
            return;
        }
    }
}
