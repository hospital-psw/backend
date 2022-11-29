namespace HospitalAPI.Dto
{
    using HospitalAPI.Dto.AppUsers;
    using System.Collections.Generic;

    public class RoomDto
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string Purpose { get; set; }
        public WorkingHoursDto WorkingHours { get; set; }
        public FloorDto Floor { get; set; }
        public int Capacity { get; set; }
        public List<ApplicationPatientDTO> Patients { get; set; }

        public RoomDto() { }
        public RoomDto(int id, string number, string purpose, WorkingHoursDto workingHours, FloorDto floor)
        {
            Id = id;
            Number = number;
            Purpose = purpose;
            WorkingHours = workingHours;
            Floor = floor;
        }
    }
}
