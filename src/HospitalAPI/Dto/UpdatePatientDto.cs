namespace HospitalAPI.Dto
{
    public class UpdatePatientDto : UpdateUserDto
    {
        public bool Hospitalized { get; set; }
    }
}
