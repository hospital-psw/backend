namespace HospitalLibrary.Core.Model.Events
{
    using HospitalLibrary.Core.Infrastucture;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ExaminationFinished : DomainEvent
    {
        public int FinishedAnamnesisId { get; set; }

        public ExaminationFinished(int aggregateId, DateTime timeStamp, string eventName, int finishedAnamnesisId) : base(aggregateId, timeStamp, eventName)
        {
            FinishedAnamnesisId = finishedAnamnesisId;
        }
    }
}
