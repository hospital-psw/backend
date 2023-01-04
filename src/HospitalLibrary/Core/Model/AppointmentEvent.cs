namespace HospitalLibrary.Core.Model
{
    using HospitalLibrary.Core.Infrastucture;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class AppointmentEvent : DomainEvent
    {
        public AppointmentEvent(int aggregateId, DateTime timeStamp, string eventName) : base(aggregateId, timeStamp, eventName)
        {
        }
    }
}
