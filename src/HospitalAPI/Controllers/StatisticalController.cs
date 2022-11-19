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
        private readonly IDoctorService _doctorService;

        public StatisticalController(IStatisticsService statisticsService, IDoctorService doctorService)
        {
            _statisticsService = statisticsService;
            _doctorService = doctorService;
        }

        [HttpGet("getStats")]
        public IActionResult GetStats()
        {
            StatisticsDTO dto = new StatisticsDTO();
            dto.Chart1 = _statisticsService.GetNumberOfAppointmentsPerMonth();
            dto.Chart2Names = _statisticsService.getDoctorNames();

            //THIS IS TEMPORARY SINCE THERE IS NO DOCTOR-PATIENT RELATION IMPLEMENTED
            List<int> temp = new List<int>();
            foreach (string doctor in dto.Chart2Names)
            {
                temp.Add(_statisticsService.getNumberOfDoctorsPatients());
            }
            dto.Chart2Values = temp;
            //THIS IS TEMPORARY SINCE THERE IS NO DOCTOR-PATIENT RELATION IMPLEMENTED

            if (dto.Chart1 is null || dto.Chart2Names is null || dto.Chart2Values is null) return NotFound();
            return Ok(dto);
        }
    }
}
