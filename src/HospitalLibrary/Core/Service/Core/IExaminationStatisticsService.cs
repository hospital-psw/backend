namespace HospitalLibrary.Core.Service.Core
{
    using HospitalLibrary.Core.DTO.ExaminationStatistics;
    using HospitalLibrary.Core.Model.Examinations;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IExaminationStatisticsService
    {
        AverageDurationDto CalculateAverageExaminationDuration();

        List<ExaminationDataDto> GetExaminationData();

        AverageStepsDto CalculateAverageSteps();

        AveragePrescriptionsDto CalculateAveragePrescriptions();

        List<SymptomDataDto> GetSymptomStats();
    }
}
