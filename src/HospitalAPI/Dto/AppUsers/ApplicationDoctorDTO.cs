namespace HospitalAPI.Dto.AppUsers
{
    using HospitalLibrary.Core.Model.Enums;

    public class ApplicationDoctorDTO : ApplicationUserDTO
    {
        public Specialization Specialization { get; set; }
        public RoomDto Office { get; set; }
        public WorkingHoursDto WorkingHours { get; set; }
    }
}
