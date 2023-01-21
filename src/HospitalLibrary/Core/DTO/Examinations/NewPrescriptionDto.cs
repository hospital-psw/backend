namespace HospitalLibrary.Core.DTO.Examinations
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class NewPrescriptionDto
    {
        [Required]
        public int MedicamentId { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime From { get; set; }

        [Required]
        public DateTime To { get; set; }

        public NewPrescriptionDto(int medicamentId, string description, DateTime from, DateTime to)
        {
            MedicamentId = medicamentId;
            Description = description;
            From = from;
            To = to;
        }

        public NewPrescriptionDto() { }
    }
}
