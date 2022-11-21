namespace HospitalAPI.Dto
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.Enums;
    using System;

    public class DateRangeDto
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        
        public DateRangeDto() { }
    }
}
