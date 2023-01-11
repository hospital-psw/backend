namespace HospitalLibrary.Core.Service
{
    using HospitalLibrary.Core.DTO.ExaminationStatistics;
    using HospitalLibrary.Core.Infrastucture;
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.Examinations;
    using HospitalLibrary.Core.Repository.Core;
    using HospitalLibrary.Core.Service.Core;
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

        public Anamnesis CalculateAverageExaminationDuration()
        {
            try
            {
                List<Anamnesis> finishedExaminations = _unitOfWork.AnamnesisRepository.GetAllFinishedAnamneses().ToList();
                Anamnesis an = CalculateDuration(finishedExaminations.ElementAt(0));
                return an;
            }
            catch(Exception e)
            {
                _logger.LogError($"Error in ExaminationStatisticsService in CalculateAverageDuration, {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        private Anamnesis CalculateDuration(Anamnesis examination)
        {
            examination.Changes.ForEach(x => Console.WriteLine(x.ToString()));

            examination.Changes.Sort(delegate(DomainEvent x, DomainEvent y)
            {
                if (x.TimeStamp > y.TimeStamp) return 1;
                else if (x.TimeStamp < y.TimeStamp) return -1;
                else return 0;
            });

            examination.Changes.ForEach(x => Console.WriteLine(x.ToString()));

            return examination;
        }
    }
}
