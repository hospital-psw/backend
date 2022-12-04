namespace HospitalAPI.Dto
{
    using HospitalLibrary.Core.Model.Enums;
    using System;

    public class RenovationRequestDisplayDto
    {
        public int Id { get; set; }
        public RenovationType renovationType { get; set; }
        public DateTime StartTime { get; set; }
        public int Duration { get; set; }
    }
}
