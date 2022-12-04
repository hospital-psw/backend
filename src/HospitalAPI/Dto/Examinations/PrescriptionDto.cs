namespace HospitalAPI.Dto.Examinations
{
    using HospitalAPI.Dto.Medicament;
    using HospitalLibrary.Core.Model.Domain;

    public class PrescriptionDto
    {
        public int Id { get; set; }

        public MedicamentDto Medicament { get; set; }

        public string Description { get; set; }

        public DateRange DateRange { get; set; }

        public PrescriptionDto(int id, MedicamentDto medicament, string description, DateRange dateRange)
        {
            Id = id;
            Medicament = medicament;
            Description = description;
            DateRange = dateRange;
        }
    }
}
