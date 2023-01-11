namespace HospitalAPI.Dto.SchedulingEvents
{
    using HospitalLibrary.Core.Model.Enums;
    using System;

    public class SchedulingRootDto
    {
        public int? Id { get; set; }
        public int PatientId { get; set; }
        public DateTime LastChanged { get; set; }
        public DateTime? DatePicked { get; set; }
        public Specialization? Specialization { get; set; }
        public int? DoctorId { get; set; }
        public DateTime? TimePicked { get; set; }

    }
}
