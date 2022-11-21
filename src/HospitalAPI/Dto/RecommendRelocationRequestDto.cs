namespace HospitalAPI.Dto
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Migrations;
    using System;

    public class RecommendRelocationRequestDto
    {
        public int FromRoomId { get; set; }
        public int ToRoomId { get; set; }
        public DateTime FromTime { get; set; }
        public DateTime ToTime { get; set; }
        public int Duration { get; set; }

    }
}
