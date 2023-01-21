namespace HospitalAPI.Dto
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Migrations;
    using System;
    using System.Collections.Generic;

    public class RecommendRelocationRequestDto
    {
        public List<int> RoomsId { get; set; }
        public DateTime FromTime { get; set; }
        public DateTime ToTime { get; set; }
        public int Duration { get; set; }

    }
}
