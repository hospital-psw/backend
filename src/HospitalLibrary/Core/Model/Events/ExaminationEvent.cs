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
        public int UserId { get; set; }
        public ExaminationEvent(int aggregateId, DateTime timeStamp, string eventName, int userId) : base(aggregateId, timeStamp, eventName)
        {
            UserId = userId;
        }
    }
}
