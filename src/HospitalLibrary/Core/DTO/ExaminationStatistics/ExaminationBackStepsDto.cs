namespace HospitalLibrary.Core.DTO.ExaminationStatistics
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ExaminationBackStepsDto
    {
        public int Id { get; set; }
        public int BackStepsNumber { get; set; }

        public ExaminationBackStepsDto() { }

        public ExaminationBackStepsDto(int id, int backStepsNumber)
        {
            Id = id;
            BackStepsNumber = backStepsNumber;
        }
    }
}
