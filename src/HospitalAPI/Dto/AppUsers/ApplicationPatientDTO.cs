namespace HospitalAPI.Dto.AppUsers
{
    using HospitalLibrary.Core.Model.Blood.Enums;

    public class ApplicationPatientDTO : ApplicationUserDTO
    {
        public bool Hospitalized { get; set; }
        public BloodType BloodType { get; internal set; }

        //public int ApplicationDoctorId{ get; set; }
    }
}
