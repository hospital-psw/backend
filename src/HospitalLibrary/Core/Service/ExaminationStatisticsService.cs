namespace HospitalLibrary.Core.Service
{
    using HospitalLibrary.Core.DTO.ExaminationStatistics;
    using HospitalLibrary.Core.Infrastucture;
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Model.Enums;
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
            catch (Exception e)
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

            foreach (DomainEvent e in examination.Changes.OrderBy(x => x.TimeStamp))
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
                    partialSum = (int)stepGap.TotalSeconds;
                }
            }
            durationSum += partialSum;
            return durationSum;
        }

        public List<ExaminationDataDto> GetExaminationData()
        {
            List<Anamnesis> finishedExaminations = _unitOfWork.AnamnesisRepository.GetAllFinishedAnamneses().ToList();
            List<ExaminationDataDto> examinationDataList = new List<ExaminationDataDto>();

            foreach (Anamnesis anamnesis in finishedExaminations)
            {
                if (anamnesis.Changes.IsNullOrEmpty()) continue;
                examinationDataList.Add(CreateExaminationData(anamnesis));
            }

            return examinationDataList;
        }

        private ExaminationDataDto CreateExaminationData(Anamnesis anamnesis)
        {
            int duration = CalculateDuration(anamnesis);
            DateTime date = anamnesis.Appointment.Date;
            string doctor = anamnesis.Appointment.Doctor.FirstName + " " + anamnesis.Appointment.Doctor.LastName;
            string patient = anamnesis.Appointment.Patient.FirstName + " " + anamnesis.Appointment.Patient.LastName;
            string specialization = anamnesis.Appointment.Doctor.Specialization.ToString();
            string type = anamnesis.Appointment.ExamType.ToString();
            int steps = anamnesis.Version;
            return new(date, doctor, patient, specialization, type, duration, steps);
        }

        public AverageStepsDto CalculateAverageSteps()
        {
            try
            {
                List<Anamnesis> finishedExaminations = _unitOfWork.AnamnesisRepository.GetAllFinishedAnamneses().ToList();
                AverageStepsDto averageSteps = new AverageStepsDto();
                int totalSteps = 0;
                finishedExaminations.Where(e => e.Version != 0).ToList().ForEach((e) => { averageSteps.Steps.Add(e.Version); totalSteps += e.Version; });
                averageSteps.CalculateAverageSteps(totalSteps);
                return averageSteps;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in ExaminationStatisticsService in CalculateAverageSteps {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public AverageBackStepsDto CalculateAverageNumberOfBackSteps()
        {
            try
            {
                List<Anamnesis> finishedExaminations = _unitOfWork.AnamnesisRepository.GetAllFinishedAnamneses().ToList();
                List<ExaminationBackStepsDto> dtoList = new List<ExaminationBackStepsDto>();
                int backStepsSum = 0;

                foreach (Anamnesis anamnesis in finishedExaminations)
                {
                    ExaminationBackStepsDto dto = new ExaminationBackStepsDto();
                    if (anamnesis.Changes.IsNullOrEmpty()) continue;
                    dto.Id = anamnesis.Id;
                    dto.BackStepsNumber = CalculateNumberOfBackSteps(anamnesis);
                    dtoList.Add(dto);
                    backStepsSum += dto.BackStepsNumber;
                }

                return new AverageBackStepsDto(dtoList, Math.Round((double)backStepsSum / dtoList.Count, 2));
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in ExaminationStatisticsService in CalculateAverageDuration, {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public AveragePrescriptionsDto CalculateAveragePrescriptions()
        {
            try
            {
                List<Anamnesis> finishedExaminations = _unitOfWork.AnamnesisRepository.GetAllFinishedAnamneses().ToList();
                AveragePrescriptionsDto averagePrescriptions = new AveragePrescriptionsDto();
                int totalSteps = 0;
                finishedExaminations.ForEach((e) => { averagePrescriptions.Prescriptions.Add(e.Prescriptions.Count); totalSteps++; });
                averagePrescriptions.CalculateAverage(totalSteps);
                return averagePrescriptions;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in ExaminationStatisticsService in CalculateAveragePrescriptions {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public AverageSymptomsDto CalculateSymptomsAverageFrequence()
        {
            try
            {
                List<Anamnesis> finishedExaminations = _unitOfWork.AnamnesisRepository.GetAllFinishedAnamneses().ToList();
                List<TemporarySymptomStatisticsDto> dtoList = new List<TemporarySymptomStatisticsDto>();

                foreach (Anamnesis anamnesis in finishedExaminations)
                {
                    foreach (Symptom symptom in anamnesis.Symptoms)
                    {
                        if (!dtoList.Exists(x => x.Name.Equals(symptom.Name)))
                        {
                            TemporarySymptomStatisticsDto dto = new TemporarySymptomStatisticsDto(symptom.Name, 1, finishedExaminations.Count);
                            dtoList.Add(dto);
                        }
                        else
                        {
                            TemporarySymptomStatisticsDto dto = dtoList.Find(x => x.Name.Equals(symptom.Name));
                            dto.Occurrences += 1;
                        }
                    }
                }

                List<SymptomStatisticsDto> sympDtoList = new List<SymptomStatisticsDto>();
                dtoList.ForEach(e =>
                {
                    SymptomStatisticsDto dto = new SymptomStatisticsDto(e.Name, Math.Round((double)e.Occurrences / e.AnamnesisNum, 2));
                    sympDtoList.Add(dto);
                });

                return new AverageSymptomsDto(sympDtoList);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in ExaminationStatisticsService in CalculateAverageDuration, {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        public List<SymptomDataDto> GetSymptomStats()
        {
            try
            {
                List<Anamnesis> finishedExaminations = _unitOfWork.AnamnesisRepository.GetAllFinishedAnamneses().ToList();
                List<Symptom> symptomList = _unitOfWork.SymptomRepository.GetAll().ToList();
                List<SymptomDataDto> symptomDataDtoList = new List<SymptomDataDto>();
                symptomList.ForEach(s =>
                {
                    symptomDataDtoList.Add(new SymptomDataDto(s.Name, CountSymptomInstances(s, finishedExaminations)));
                });
                return symptomDataDtoList;
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in ExaminationStatisticsService in CalculateAveragePrescriptions {e.Message} in {e.StackTrace}");
                return null;
            }
        }

        private int CountSymptomInstances(Symptom s, List<Anamnesis> finishedExaminations)
        {
            return finishedExaminations.Where(x => x.Symptoms.Any(symy => symy.Id == s.Id)).ToList().Count;
        }

        private int CalculateNumberOfBackSteps(Anamnesis examination)
        {
            int backSteps = 0;

            foreach (DomainEvent e in examination.Changes.OrderBy(x => x.TimeStamp))
            {
                if (e.EventName.Contains("EXAMINATION_PREVIOUS"))
                {
                    backSteps += 1;
                }
            }

            return backSteps;
        }

        public AverageSpecializationDurationDto CalculateAverageExaminationDurationBySpec()
        {
            try
            {
                List<Anamnesis> finishedExaminations = _unitOfWork.AnamnesisRepository.GetAllFinishedAnamneses().ToList();
                List<ExaminationDurationDto> examDtoList = new List<ExaminationDurationDto>();
                List<TemporarySpecStatisticsDto> tempDtoList = new List<TemporarySpecStatisticsDto>();

                foreach (Anamnesis anamnesis in finishedExaminations)
                {
                    ExaminationDurationDto dto = new ExaminationDurationDto();
                    if (anamnesis.Changes.IsNullOrEmpty()) continue;
                    dto.Id = anamnesis.Id;
                    dto.Duration = CalculateDuration(anamnesis);
                    examDtoList.Add(dto);
                    Specialization currentSpec = anamnesis.Appointment.Doctor.Specialization;

                    if (!tempDtoList.Exists(x => x.Specialization == currentSpec))
                    {
                        TemporarySpecStatisticsDto specDto = new TemporarySpecStatisticsDto(currentSpec, dto.Duration, 1);
                        tempDtoList.Add(specDto);
                    }
                    else
                    {
                        TemporarySpecStatisticsDto specDto = tempDtoList.Find(x => x.Specialization == currentSpec);
                        specDto.DurationSum += dto.Duration;
                        specDto.AnamnesisNum += 1;
                    }
                }

                List<SpecializationStatisticsDto> specDtoList = new List<SpecializationStatisticsDto>();
                tempDtoList.ForEach(dto =>
                {
                    SpecializationStatisticsDto specDto = new SpecializationStatisticsDto(Math.Round((double)dto.DurationSum / dto.AnamnesisNum, 2), dto.Specialization);
                    specDtoList.Add(specDto);
                });

                return new AverageSpecializationDurationDto(examDtoList, specDtoList);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in ExaminationStatisticsService in CalculateAverageDuration, {e.Message} in {e.StackTrace}");
                return null;
            }
        }

    }
}
