namespace HospitalLibrary.Core.DTO.Examinations
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class NewAnamnesisDto
    {
        [Required]
        public int AppointmentId { get; set; }

        [Required]
        public string Description { get; set; }

        public List<NewPrescriptionDto> NewPrescriptions { get; set; }

        public List<int> SymptomIds { get; set; }
    }
}
