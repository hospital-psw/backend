namespace HospitalAPI.Dto.SchedulingEvents
{
    using HospitalAPI.Dto.SchedulingEvents.Enum;
    using HospitalLibrary.Core.Model.Enums;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class SpecializationSelectedDto : SchedulingEventDto
    {
        [Required]
        public int PatientId { get; set; }
        [Required]
        public Specialization Specialization { get; set; }
        public SpecializationSelectedDto(int aggregateId, SchedulingEventType eventType, DateTime timeStamp, Specialization specialization, int patientId) : base(aggregateId, eventType, timeStamp)
        {
            PatientId = patientId; 
            Specialization = specialization;
        }
    }
}
