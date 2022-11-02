namespace HospitalAPI.Dto
{
    using System;

    public class WorkingHoursDto
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public WorkingHoursDto() { }

    }
}
