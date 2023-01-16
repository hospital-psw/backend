namespace HospitalLibrary.Core.DTO.ExaminationStatistics
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class AveragePrescriptionsDto
    {
        public List<int> Prescriptions { get; set; }

        public double Average { get; set; }

        public AveragePrescriptionsDto()
        {
            Prescriptions = new List<int>();
        }

        public void CalculateAverage(int totalSteps)
        {
            Average = Math.Round((double)totalSteps / Prescriptions.Count, 2);
        }
    }
}
