namespace HospitalAPI.Dto
{

    public class RoomDto
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string Purpose { get; set; }
        public WorkingHoursDto WorkingHours { get; set; }
        public FloorDto Floor { get; set; }

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
