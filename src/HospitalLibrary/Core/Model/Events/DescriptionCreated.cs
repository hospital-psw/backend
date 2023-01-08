namespace HospitalLibrary.Core.Model.Events
{
    using HospitalLibrary.Core.Infrastucture;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class DescriptionCreated : DomainEvent
    {
        public string Description { get; set; }
        public DescriptionCreated(int aggregateId, DateTime timeStamp, string eventName, string description) : base(aggregateId, timeStamp, eventName)
        {
            Description = description;
        }
    }
}
