namespace HospitalAPI.Dto
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Migrations;
    using System;

    public class RecommendRelocationRequestDto
    {
        public RoomDto FromRoom { get; set; }
        public RoomDto ToRoom { get; set; }
        public Equipment Equipment { get; set; }
        public int Quantity { get; set; }
        public DateTime FromTime { get; set; }
        public DateTime ToTime { get; set; }
        public int Duration { get; set; }

    }
}
