namespace HospitalAPI.Dto.Therapy
{
    public class NewBloodUnitTherapyDto
    {

        public int BloodUnitId { get; set; }

        public int Amount { get; set; }

        public string About { get; set; }

        public int MedicalTreatmentId { get; set; }

    }
}
