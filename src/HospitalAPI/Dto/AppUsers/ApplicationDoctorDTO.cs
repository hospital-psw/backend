namespace HospitalAPI.Dto.AppUsers
{
    using HospitalLibrary.Core.Model.Enums;

    public class ApplicationDoctorDTO : ApplicationUserDTO
    {
        public Specialization Specialization { get; set; }
    }
}
