namespace HospitalLibrary.Core.DTO.ExaminationStatistics
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class AverageStepsDto
    {
        public List<int> Steps { get; set; }
        public double AverageSteps { get; set; }

        public AverageStepsDto()
        {
            Steps = new List<int>();
        }

        public AverageStepsDto(List<int> steps, double avg)
        {
            Steps = steps;
            AverageSteps = avg;
        }

        public void CalculateAverageSteps(int totalSteps)
        {
            AverageSteps = Math.Round((double)totalSteps / Steps.Count, 2);
        }
    }
}
