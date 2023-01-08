namespace HospitalAPI.Dto
{
    using System;

    public class DoctorOptionalStatisticDto
    {
        public int DoctorId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public DoctorOptionalStatisticDto() { }
        public DoctorOptionalStatisticDto(int doctorId, DateTime start, DateTime end)
        {
            this.DoctorId = doctorId;
            this.Start = start;
            this.End = end;
        }
    }
}
