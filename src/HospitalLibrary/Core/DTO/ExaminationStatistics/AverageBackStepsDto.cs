namespace HospitalLibrary.Core.DTO.ExaminationStatistics
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class AverageBackStepsDto
    {
        public List<ExaminationBackStepsDto> ExaminationBackSteps { get; set; }

        public double AverageBackStepsNumber { get; set; }

        public AverageBackStepsDto()
        {
            ExaminationBackSteps = new List<ExaminationBackStepsDto>();
        }

        public AverageBackStepsDto(List<ExaminationBackStepsDto> examinationBackSteps, double averageBackStepsNumber)
        {
            ExaminationBackSteps = examinationBackSteps;
            AverageBackStepsNumber = averageBackStepsNumber;
        }
    }
}
