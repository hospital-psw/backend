namespace HospitalAPI.Dto.Consilium
{
    using System;

    public class DisplayConsiliumDto
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public int Duration { get; set; }
        public string Topic { get; set; }
        public RoomDto Room { get; set; }
    }
}
