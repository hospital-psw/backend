namespace HospitalAPI.Dto
{
    using HospitalLibrary.Core.Model.Enums;

    public class DoctorDto : UserDto
    {
        public Specialization Specialization { get; set; }
        public RoomDto Office { get; set; }
        public WorkingHoursDto WorkingHours { get; set; }
    }
}
