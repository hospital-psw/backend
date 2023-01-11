namespace HospitalLibrary.Core.Model.Events
{
    using HospitalLibrary.Core.Infrastucture;
    using HospitalLibrary.Core.Model.Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class SymptomsChanged : DomainEvent
    {

        public int UserId { get; set; }
        public int SymptomId { get; set; }

        public SymptomEventStatus Status { get; set; }

        public SymptomsChanged(int aggregateId, DateTime timeStamp, string eventName, int symptomId, SymptomEventStatus status, int userId) : base(aggregateId, timeStamp, eventName)
        {
            SymptomId = symptomId;
            Status = status;
            UserId = userId;
        }
    }
}
