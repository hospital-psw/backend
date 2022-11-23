namespace HospitalAPI.Dto
{
    using HospitalLibrary.Core.Model.Blood.Enums;

    public class PatientDto : UserDto
    {
        public bool Hospitalized { get; set; }
        public BloodType BloodType { get; internal set; }
    }
}
