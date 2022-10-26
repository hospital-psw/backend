namespace HospitalLibrary.Core.DTO
{
    using HospitalLibrary.Core.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class WorkigHoursDTO
    {

        public string Start { get; set; }
        public string End { get; set; }

        public WorkigHoursDTO()
        {
        }

        public WorkigHoursDTO(WorkingHours workingHours)
        {
            Start = workingHours.Start.Hour.ToString() + ":" + workingHours.Start.Minute.ToString();
            End = workingHours.End.Hour.ToString() + ":" + workingHours.End.Minute.ToString();
        }
    }
}
