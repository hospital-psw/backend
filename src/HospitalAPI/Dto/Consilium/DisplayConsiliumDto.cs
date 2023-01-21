namespace HospitalAPI.Dto.Consilium
{
    using System;

    public class DisplayConsiliumDto
    {
        public int Id { get; set; }
        public DateTime StartDateTime { get; set; }
        public int Duration { get; set; }
        public DateTime EndDateTime { get; set; }
        public string Topic { get; set; }
        public RoomDto Room { get; set; }
    }
}
