namespace HospitalLibrary.Core.Model.Events.Scheduling
{
    using HospitalLibrary.Core.Infrastucture;
    using HospitalLibrary.Core.Model.Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class SpecializationSelected : DomainEvent
    {
        public Specialization Specialization { get; set; }
        public int PatientId { get; set; }
        public SpecializationSelected(int aggregateId, DateTime timeStamp, string eventName, Specialization specialization, int patientId) : base(aggregateId, timeStamp, eventName)
        {
            Specialization = specialization;
            PatientId = patientId;
        }
    }
}
