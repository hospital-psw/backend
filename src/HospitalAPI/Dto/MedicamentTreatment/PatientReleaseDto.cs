namespace HospitalAPI.Dto.MedicamentTreatment
{
    public class PatientReleaseDto
    {
        public string Description { get; set; }

        public int TreatmentId { get; set; }

        public PatientReleaseDto()
        {

        }

        public PatientReleaseDto(string description, int id)
        {
            Description = description;
            TreatmentId = id;
        }
    }
}
