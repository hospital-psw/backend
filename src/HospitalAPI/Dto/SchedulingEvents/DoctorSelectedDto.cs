namespace HospitalAPI.Dto.SchedulingEvents
{
    using HospitalAPI.Dto.SchedulingEvents.Enum;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class DoctorSelectedDto : SchedulingEventDto
    {
        [Required]
        public int PatientId { get; set; }
        [Required]
        public int DoctorId { get; set; }
        public DoctorSelectedDto(int aggregateId, SchedulingEventType eventType, DateTime timeStamp, int patientId, int doctorId) : base(aggregateId, eventType, timeStamp)
        {
            PatientId = patientId;
            DoctorId = doctorId;
        }


    }
}
