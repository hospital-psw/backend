namespace HospitalAPI.Dto
{
    using HospitalLibrary.Core.Model.ValueObjects;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CancelAppointmentDto
    {
        [Required]
        public int AppointmentId { get; set; }

        [Required]
        public CancellationInfo CancellationInfo { get; set; }
    }
}
