namespace HospitalAPI.Controllers
{
    using HospitalAPI.Dto;
    using HospitalLibrary.Core.Service.Core;
    using Microsoft.AspNetCore.Mvc;
    using System;

    [ApiController]
    [Route("api/[controller]")]
    public class RelocationController : ControllerBase
    {
        private IRelocationService _relocationService;

        public RelocationController(IRelocationService relocationService)
        {
            _relocationService = relocationService;
        }

        public IActionResult Create()
        {
            throw new NotImplementedException();
        }

        [HttpGet("getRecommendedAppointments")]
        public IActionResult GetRecommendedRelocationAppointments(RecommendRelocationRequestDto dto)
        {
            return Ok(_relocationService.GetAvailableAppointments(dto.FromRoom.Id, dto.ToRoom.Id, dto.FromTime, dto.ToTime, dto.Duration));
        }
    }
}
