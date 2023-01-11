namespace HospitalLibrary.Core.Service
{
    using HospitalLibrary.Core.DTO.ExaminationStatistics;
    using HospitalLibrary.Core.Infrastucture;
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.Examinations;
    using HospitalLibrary.Core.Repository.Core;
    using HospitalLibrary.Core.Service.Core;
    using IdentityServer4.Extensions;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ExaminationStatisticsService : BaseService<Anamnesis>, IExaminationStatisticsService
    {
        private readonly ILogger<ExaminationStatisticsService> _logger;

        public ExaminationStatisticsService(IUnitOfWork unitOfWork, ILogger<ExaminationStatisticsService> logger) : base(unitOfWork)
        {
            _logger = logger;
        }

        public AverageDurationDto CalculateAverageExaminationDuration()
        {
            try
            {
                List<Anamnesis> finishedExaminations = _unitOfWork.AnamnesisRepository.GetAllFinishedAnamneses().ToList();
                List<ExaminationDurationDto> dtoList = new List<ExaminationDurationDto>();
                int durationSum = 0;
                
                foreach (Anamnesis anamnesis in finishedExaminations)
                {
                    ExaminationDurationDto examinationDurationDto = new ExaminationDurationDto();
                    if (anamnesis.Changes.IsNullOrEmpty()) continue;
                    examinationDurationDto.Id = anamnesis.Id;
                    examinationDurationDto.Duration = CalculateDuration(anamnesis);
                    dtoList.Add(examinationDurationDto);

                    durationSum += examinationDurationDto.Duration;
                }

                return new AverageDurationDto(dtoList, Math.Round((double)durationSum / dtoList.Count, 2));
            }
            catch(Exception e)
            {
                _logger.LogError($"Error in ExaminationStatisticsService in CalculateAverageDuration, {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        private int CalculateDuration(Anamnesis examination)
        {

            DateTime firstStep = default(DateTime);
            DateTime lastStep = default(DateTime);
            int durationSum = 0;
            int partialSum = 0;

            foreach(DomainEvent e in examination.Changes.OrderBy(x => x.TimeStamp))
            {

                if (e.EventName.Equals("EXAMINATION_STARTED"))
                {
                    durationSum += partialSum;
                    partialSum = 0;
                    firstStep = e.TimeStamp;
                } 
                else
                {
                    lastStep = e.TimeStamp;
                    TimeSpan stepGap = lastStep - firstStep;
                    if (stepGap.TotalMinutes > 15) continue;
                    partialSum = (int) stepGap.TotalSeconds;
                }
            }
            durationSum += partialSum;
            return durationSum;
        }
    }
}
