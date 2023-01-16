namespace HospitalLibrary.Core.Model.Events.Scheduling
{
    using HospitalLibrary.Core.Infrastucture;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class NextClicked : DomainEvent
    {
        public int Step { get; set; }
        public int PatientId { get; set; }

        public NextClicked(int aggregateId, DateTime timeStamp, string eventName, int step, int patientId) : base(aggregateId, timeStamp, eventName)
        {
            Step = step;
            PatientId = patientId;
        }
    }
}
