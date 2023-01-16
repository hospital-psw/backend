namespace HospitalLibrary.Core.Model.Events.Scheduling
{
    using HospitalLibrary.Core.Infrastucture;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class SessionStarted : DomainEvent
    {
        public int PatientId { get; set; }
        public SessionStarted(int aggregateId, DateTime timeStamp, string eventName, int patientId) : base(aggregateId, timeStamp, eventName)
        {
            PatientId = patientId;
        }
    }
}
