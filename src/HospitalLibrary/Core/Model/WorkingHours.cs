namespace HospitalLibrary.Core.Model
{
    using HospitalLibrary.Core.DTO;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class WorkingHours : Entity
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public WorkingHours()
        {
        }

        public WorkingHours(WorkigHoursDTO workingHourDTO)
        {
            this.Start = DateTime.Parse(workingHourDTO.Start);
            this.End = DateTime.Parse(workingHourDTO.End);
        }

        public WorkingHours(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }
    }
}
