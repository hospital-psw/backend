namespace HospitalLibrary.Core.Model.Events
{
    using HospitalLibrary.Core.Infrastucture;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class PrescriptionRemoved : DomainEvent
    {
        public int PrescriptionId { get; set; }
        public PrescriptionRemoved(int aggregateId, DateTime timeStamp, string eventName, int prescriptionId) : base(aggregateId, timeStamp, eventName)
        {
            PrescriptionId = prescriptionId;
        }
    }
}
