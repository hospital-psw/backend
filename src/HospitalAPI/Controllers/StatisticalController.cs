namespace HospitalAPI.Controllers
{
    using HospitalAPI.Dto;
    using HospitalAPI.Dto.Statistics;
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Service;
    using HospitalLibrary.Core.Service.Core;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [Route("api/[controller]")]
    [ApiController]
    public class StatisticalController : ControllerBase
    {
        private readonly IStatisticsService _statisticsService;

        public StatisticalController(IStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }

        [HttpGet("getStats")]
        public IActionResult GetStats()
        {
            StatisticsDTO dto = new StatisticsDTO();
            dto.Chart1 = _statisticsService.GetNumberOfAppointmentsPerMonth();
            (dto.Chart2Names, dto.Chart2Values) = _statisticsService.GetPatientsPerDoctor();
            (dto.Chart3Male, dto.Chart3Female) = _statisticsService.GetNumberOfPatientsByAgeGroup();
            dto.Chart4 = _statisticsService.GetUsersByType();

            if (dto.Chart1 is null || dto.Chart2Names is null || dto.Chart2Values is null || dto.Chart3Male is null || dto.Chart3Female is null || dto.Chart4 is null) return NotFound("Something went wrong :("); //TODO: better error handling
            return Ok(dto);
        }
    }
}
