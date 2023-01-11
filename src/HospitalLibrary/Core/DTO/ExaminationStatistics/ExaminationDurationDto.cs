namespace HospitalLibrary.Core.DTO.ExaminationStatistics
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ExaminationDurationDto
    {
        public int Id { get; set; }

        public int Duration { get; set; }

        public ExaminationDurationDto()
        {

        }

        public ExaminationDurationDto(int id, int duration)
        {
            Duration = duration;
            Id = id;
        }
    }
}
