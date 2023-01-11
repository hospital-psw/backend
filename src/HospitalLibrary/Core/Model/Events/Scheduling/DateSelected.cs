namespace HospitalLibrary.Core.Model.Events.Scheduling
{
    using HospitalLibrary.Core.Infrastucture;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class DateSelected : DomainEvent
    {
        public DateTime Date { get; set; }
        public int PatientId { get; set; }
        public DateSelected(int aggregateId, DateTime timeStamp, string eventName, DateTime date, int patientId) : base(aggregateId, timeStamp, eventName)
        {
            Date = date;
            PatientId = patientId;
        }
    }
}
