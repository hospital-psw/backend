namespace HospitalAPI.Dto
{
    using HospitalAPI.Dto.Enum;
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.Enums;
    using System;
    using System.Collections.Generic;

    public class RenovationEventDto
    {
        public int AggregateId;
        public RenovationEventType EventType;
        public DateTime TimeStamp;
        public RenovationType Type;
    }
}
