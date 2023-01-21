namespace HospitalAPI.Dto
{
    using HospitalAPI.Dto.Enum;
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.Enums;
    using System;
    using System.Collections.Generic;

    public class RenovationEventDto
    {
        public int AggregateId { get; set; }
        public RenovationEventType EventType { get; set; }
        public DateTime TimeStamp { get; set; }
        public RenovationType Type { get; set; }

        public RenovationEventDto(int aggregateId, RenovationEventType eventType, DateTime timeStamp, RenovationType type)
        {
            AggregateId = aggregateId;
            EventType = eventType;
            TimeStamp = timeStamp;
            Type = type;
        }
    }
}
