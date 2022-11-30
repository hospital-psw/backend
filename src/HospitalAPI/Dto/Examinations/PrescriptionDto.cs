namespace HospitalAPI.Dto.Examinations
{
    using HospitalAPI.Dto.Medicament;
    using HospitalLibrary.Core.Model.Domain;

    public class PrescriptionDto
    {
        public MedicamentDto Medicament { get; set; }   

        public string Description { get; set; }

        public DateRange DateRange { get; set; }

        public PrescriptionDto(MedicamentDto medicament, string description, DateRange dateRange)
        {
            Medicament = medicament;
            Description = description;
            DateRange = dateRange;
        }
    }
}
