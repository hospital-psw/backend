namespace HospitalAPI.Dto
{
    public class UpdatePatientDto : UpdateUserDto
    {
        public bool Guest { get; set; }
    }
}