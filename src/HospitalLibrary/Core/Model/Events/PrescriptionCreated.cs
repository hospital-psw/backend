namespace HospitalLibrary.Core.Model.Events
{
    using HospitalLibrary.Core.DTO.Examinations;
    using HospitalLibrary.Core.Infrastucture;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class PrescriptionCreated : DomainEvent
    {
        public int MedicamentId { get; set; }

        public string Description { get; set; }

        public DateTime From { get; set; }

        public DateTime To { get; set; }
        public PrescriptionCreated(int aggregateId, DateTime timeStamp, string eventName, int medicamentId, string description, DateTime from, DateTime to) : base(aggregateId, timeStamp, eventName)
        {
            MedicamentId = medicamentId;
            Description = description;
            From = from;
            To = to;
        }
    }
}
