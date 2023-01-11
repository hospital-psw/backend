namespace HospitalLibrary.Core.Model.Events.Scheduling
{
    using HospitalLibrary.Core.Infrastucture;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class AppointmentSelected : DomainEvent
    {
        public DateTime DateTime { get; set; }
        public int PatientId { get; set; }
        public AppointmentSelected(int aggregateId, DateTime timeStamp, string eventName, DateTime dateTime, int patientId) : base(aggregateId, timeStamp, eventName)
        {
            DateTime = dateTime;
            PatientId = patientId;
        }
    }
}
