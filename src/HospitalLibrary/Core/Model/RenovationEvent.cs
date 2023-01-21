namespace HospitalLibrary.Core.Model
{
    using HospitalLibrary.Core.Infrastucture;
    using HospitalLibrary.Core.Model.Enums;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class RenovationEvent : DomainEvent
    {
        public RenovationType Type { get; set; }
        public RenovationEvent(int aggregateId, DateTime timeStamp, string eventName, RenovationType type) : base(aggregateId, timeStamp, eventName)
        {
            Type = type;
        }
    }
}
