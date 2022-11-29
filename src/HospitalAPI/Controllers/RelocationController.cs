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
        private IRoomScheduleService _roomScheduleService;

        public RelocationController(IRelocationService relocationService, IRoomService roomService, IEquipmentService equipmentService, IRoomScheduleService roomScheduleService)
        {
            _relocationService = relocationService;
            _roomService = roomService;
            _equipmentService = equipmentService;
            _roomScheduleService = roomScheduleService;
        }

        [HttpPost("createRelocationRequest")]
        public IActionResult Create([FromBody] RelocationRequestDto dto)
        {
            return Ok(_relocationService.Create(RelocationRequestMapper.EntityDtoToEntity(dto, _roomService.GetById(dto.FromRoomId), _roomService.GetById(dto.ToRoomId), _equipmentService.Get(dto.EquipmentId))));
        }

        [HttpPut("recommend")]
        public IActionResult GetFreeTimeSlots([FromBody] RecommendRelocationRequestDto dto)
        {
            return Ok(_roomScheduleService.GetAppointments(dto.RoomsId, dto.FromTime, dto.ToTime, dto.Duration));
        }
    }
}
