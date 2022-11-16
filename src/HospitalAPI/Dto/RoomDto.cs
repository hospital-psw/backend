namespace HospitalAPI.Dto
{
    using System.Collections.Generic;

    public class RoomDto
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string Purpose { get; set; }
        public WorkingHoursDto WorkingHours { get; set; }
        public FloorDto Floor { get; set; }
        public int Capacity { get; set; }
        public List<PatientDto> Patients { get; set; }

    }
}
