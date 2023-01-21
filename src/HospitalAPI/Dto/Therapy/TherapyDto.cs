namespace HospitalAPI.Dto.Therapy
{
    using HospitalLibrary.Core.Model.Enums;
    using System;

    public class TherapyDto
    {

        public int Id { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public string About { get; set; }

        public TherapyType Type { get; set; }
    }
}
