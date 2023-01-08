namespace HospitalLibrary.Core.Model.Events
{
    using HospitalLibrary.Core.Infrastucture;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ExaminationEvent : DomainEvent
    {
        public ExaminationEvent(int aggregateId, DateTime timeStamp, string eventName) : base(aggregateId, timeStamp, eventName)
        {
        }
    }
}
