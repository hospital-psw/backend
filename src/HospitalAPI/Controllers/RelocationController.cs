namespace HospitalAPI.Controllers
{
    using HospitalAPI.Dto;
    using HospitalAPI.Mappers;
    using HospitalLibrary.Core.Service.Core;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

    [ApiController]
    [Route("api/[controller]")]
    public class RelocationController : ControllerBase
    {
        private IRelocationService _relocationService;
        private IRoomService _roomService;
        private IEquipmentService _equipmentService;

        public RelocationController(IRelocationService relocationService, IRoomService roomService, IEquipmentService equipmentService)
        {
            _relocationService = relocationService;
            _roomService = roomService;
            _equipmentService = equipmentService;
        }

        [HttpPost("createRelocationRequest")]
        public IActionResult Create([FromBody] RelocationRequestDto dto)
        {
            return Ok(_relocationService.Create(RelocationRequestMapper.EntityDtoToEntity(dto, _roomService.GetById(dto.FromRoomId), _roomService.GetById(dto.ToRoomId), _equipmentService.Get(dto.EquipmentId))));
        }

        [HttpPut("recommend")]
        public IActionResult GetRecommendedRelocationAppointments([FromBody] RecommendRelocationRequestDto dto)
        {
            DateTime startTime = new DateTime(dto.FromTime.Year, dto.FromTime.Month, dto.FromTime.Day, 7, 0, 0);
            DateTime toTime = new DateTime(dto.ToTime.Year, dto.FromTime.Month, dto.FromTime.Day, 22, 0, 0);
            return Ok(_relocationService.GetAvailableAppointments(dto.FromRoomId, dto.ToRoomId, startTime, toTime, dto.Duration));
        }
    }
}
