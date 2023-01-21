namespace HospitalAPI.Controllers
{
    using HospitalAPI.Dto;
    using HospitalAPI.Dto.Statistics;
    using HospitalLibrary.Core.DTO.ExaminationStatistics;
    using HospitalLibrary.Core.DTO.RenovationRequest;
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Service;
    using HospitalLibrary.Core.Service.AppUsers.Core;
    using HospitalLibrary.Core.Service.Core;
    using IdentityServer4.Extensions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using PagedList;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [Route("api/[controller]")]
    [ApiController]
    public class StatisticalController : ControllerBase
    {
        private readonly IStatisticsService _statisticsService;
        private readonly IExaminationStatisticsService _examinationStatisticsService;

        public StatisticalController(IStatisticsService statisticsService, IExaminationStatisticsService examinationStatisticsService)
        {
            _statisticsService = statisticsService;
            _examinationStatisticsService = examinationStatisticsService;
        }

        [HttpGet("getStats")]
        public IActionResult GetStats()
        {
            StatisticsDTO dto = new StatisticsDTO();
            dto.Chart1 = _statisticsService.GetNumberOfAppointmentsPerMonth();
            (dto.Chart2Names, dto.Chart2Values) = _statisticsService.GetPatientsPerDoctor();
            (dto.Chart3Male, dto.Chart3Female) = _statisticsService.GetNumberOfPatientsByAgeGroup();
            dto.Chart4 = _statisticsService.GetUsersByType();

            if (dto.Chart1 is null || dto.Chart2Names is null || dto.Chart2Values is null || dto.Chart3Male is null || dto.Chart3Female is null || dto.Chart4 is null) return NotFound("Something went wrong :(");
            return Ok(dto);
        }

        [HttpGet("getVacationStats/{doctorId}")]
        public IActionResult GetVacationStatistics(int doctorId)
        {
            List<int> result = (List<int>)_statisticsService.GetNumberOfVacationDaysPerMonth(doctorId);
            return Ok(result);
        }

        [HttpGet("getRenovationStats/duration")]
        public IActionResult GetAverageSchedulingDuration()
        {
            return Ok(_statisticsService.GetAverageSchedulingDuration());
        }

        [HttpGet("getRenovationStats/duration/groups")]
        public IActionResult GetAverageSchedulingDurationByGroups()
        {
            return Ok(_statisticsService.GetAverageSchedulingDurationByGroups());
        }




        [HttpGet("getYearlyDoctorAppointmentsStats/{doctorId}/{year}")]
        public IActionResult GetYearlyDoctorAppointmentsStatistics(int doctorId, int year)
        {
            return Ok(_statisticsService.GetNumberOfDoctorAppointmentsPerYear(doctorId, year));
        }

        [HttpGet("stats/doctor/month/{doctorId}/{month}/{year}")]
        public IActionResult GetMonthlyDoctorAppointmentsStatistics(int doctorId, int month, int year)
        {
            return Ok(_statisticsService.GetNumberOfDoctorAppointmentsPerMonth(doctorId, month, year));
        }


        [HttpGet("getNumberOfViewsForEachStep")]
        public IActionResult GetNumberOfViewsForEachStep()
        {
            return Ok(_statisticsService.GetNumberOfViewsForEachStep());
        }

        [HttpGet("getNumberOfStepsAccordingToRenovationType")]
        public IActionResult GetNumberOfStepsAccordingToRenovationType()
        {
            return Ok(_statisticsService.GetNumberOfStepsAccordingToRenovationType());
        }

        [HttpGet("getAverageNumberOfRenovationSteps")]
        public IActionResult GetAverageNumberOfRenovationSteps()
        {
            return Ok(_statisticsService.GetNumberOfStepsAccordingToRenovationType());
        }

        [HttpGet("getAverageDurationAccordingToRenovationType")]
        public IActionResult GetAverageDurationAccordingToRenovationType()
        {
            return Ok(_statisticsService.GetAverageSchedulingDurationBasedOnRenovationType());
        }

        [HttpGet("getTimeSpentPerStep")]
        public IActionResult GetTimeSpentPerStep()
        {
            List<RenovationStatisticDto> dto = _statisticsService.GetTimeSpentPerStep();
            return Ok(dto);
        }

        [HttpPost("getOptionalDoctorStats")]
        public IActionResult GetOptionalDoctorAppointmentsStatistics(DoctorOptionalStatisticDto dto)
        {
            return Ok(_statisticsService.GetNumberOfDoctorAppointmentsInOptionalTimeRange(dto.DoctorId, dto.Start, dto.End));
        }

        [HttpGet("examination/average-duration")]
        public IActionResult GetAverageExaminationDuration()
        {
            AverageDurationDto dto = _examinationStatisticsService.CalculateAverageExaminationDuration();

            if (dto == null)
            {
                NoContent();
            }

            return Ok(dto);
        }

        [HttpGet("examinaton/average-steps")]
        public IActionResult GetAverageExaminationSteps()
        {
            return Ok(_examinationStatisticsService.CalculateAverageSteps());
        }

        [HttpGet("examination/average-prescriptions")]
        public IActionResult GetAverageExaminationPrescriptions()
        {
            return Ok(_examinationStatisticsService.CalculateAveragePrescriptions());
        }

        [HttpGet("examination/symptom-count")]
        public IActionResult GetExaminationSymptomFrequency()
        {
            AverageSymptomsDto dto = _examinationStatisticsService.CalculateSymptomsAverageFrequence();

            if (dto == null)
            {
                NoContent();
            }

            return Ok(dto);
        }

        [HttpGet("examination/average-back-steps")]
        public IActionResult GetAverageBackSteps()
        {
            AverageBackStepsDto dto = _examinationStatisticsService.CalculateAverageNumberOfBackSteps();

            if (dto == null)
            {
                NoContent();
            }

            return Ok(dto);
        }

        [HttpGet("examination/specialization/average-duration")]
        public IActionResult GetAverageDurationBySpecialization()
        {
            AverageSpecializationDurationDto dto = _examinationStatisticsService.CalculateAverageExaminationDurationBySpec();

            if (dto == null)
            {
                NoContent();
            }

            return Ok(dto);
        }

        [HttpGet("examination/data/{pageSize}/{pageNumber}")]
        public IActionResult GetExaminatonData(int pageSize, int pageNumber)
        {
            if (pageNumber == 0)
            {
                pageNumber = 1;
            }

            List<ExaminationDataDto> dtoList = _examinationStatisticsService.GetExaminationData();

            if (dtoList.IsNullOrEmpty())
            {
                return NoContent();
            }

            return Ok(dtoList.ToPagedList(pageNumber, pageSize));
        }

    }
}
