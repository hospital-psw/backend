namespace HospitalAPI.Dto.Therapy
{
    using HospitalAPI.Dto.Medicament;

    public class MedicamentTherapyDto : TherapyDto
    {

        public MedicamentDto Medicament { get; set; }

        public int Amount { get; set; }
    }
}
