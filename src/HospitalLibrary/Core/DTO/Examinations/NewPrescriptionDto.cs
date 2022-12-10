namespace HospitalLibrary.Core.DTO.Examinations
{
    using System;

    public class NewPrescriptionDto
    {
        public int MedicamentId { get; set; }

        public string Description { get; set; }

        public DateTime From { get; set; }

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
