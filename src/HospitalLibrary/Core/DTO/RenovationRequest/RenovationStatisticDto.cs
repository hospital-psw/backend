namespace HospitalLibrary.Core.DTO.RenovationRequest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class RenovationStatisticDto
    {
        public List<DateTime> Dates { get; set; }
        public List<double> TimeSpentPerStep { get; set; }

        public RenovationStatisticDto() { }

        public RenovationStatisticDto(List<DateTime> dates, List<double> timeSpentPerStep)
        {
            this.Dates = dates;
            this.TimeSpentPerStep = timeSpentPerStep;
        }
    }
}
