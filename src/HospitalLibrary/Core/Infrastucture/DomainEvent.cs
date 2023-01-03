namespace HospitalLibrary.Core.Infrastucture
{
    using HospitalLibrary.Core.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public abstract class DomainEvent : Entity
    {
        public int AggregateId { get; private set; }
        public string EventName { get; private set; }
        public DateTime TimeStamp { get; private set; }


        public DomainEvent(int aggregateId, DateTime timeStamp, string eventName)
        {
            AggregateId = aggregateId;
            TimeStamp = timeStamp;
            EventName = eventName;
        }
    }
}
