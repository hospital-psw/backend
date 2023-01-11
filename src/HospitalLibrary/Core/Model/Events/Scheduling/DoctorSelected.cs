namespace HospitalLibrary.Core.Model.Events.Scheduling
{
    using HospitalLibrary.Core.Infrastucture;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class DoctorSelected : DomainEvent
    {
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public DoctorSelected(int aggregateId, DateTime timeStamp, string eventName, int doctorId, int patientId) : base(aggregateId, timeStamp, eventName)
        {
            DoctorId = doctorId;
            PatientId = patientId;
        }
    }
}
