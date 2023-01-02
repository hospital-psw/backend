namespace HospitalAPI.Controllers
{
    using HospitalAPI.Dto;
    using HospitalAPI.Mappers;
    using HospitalAPI.Mappers.Renovation;
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Service;
    using HospitalLibrary.Core.Service.Core;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

    [ApiController]
    [Route("api/[controller]")]
    public class RenovationController : ControllerBase
    {
        private IRenovationService _renovationService;
        private IRoomService _roomService;
        private IRoomScheduleService _roomScheduleService;

        public RenovationController(IRenovationService renovationService, IRoomService roomService, IRoomScheduleService roomScheduleService)
        {
            _renovationService = renovationService;
            _roomService = roomService;
            _roomScheduleService = roomScheduleService;
        }

        [HttpPost("createRenovationRequest")]
        public IActionResult Create([FromBody] RenovationRequestDto dto)
        {
            List<Room> rooms = new List<Room>();
            foreach (int roomId in dto.RoomsId)
            {
                rooms.Add(_roomService.GetById(roomId));
            }
            RenovationRequest request = RenovationRequestMapper.EntityDtoToEntity(dto, rooms);
            request.Id= dto.Id;
            return Ok(_renovationService.Create(request));
        }

        [HttpPut("recommend")]
        public IActionResult GetFreeTimeSlots([FromBody] RecommendRelocationRequestDto dto)
        {
            return Ok(_roomScheduleService.GetAppointments(dto.RoomsId, dto.FromTime, dto.ToTime, dto.Duration));
        }

        [HttpGet("{roomId}")]
        public IActionResult GetAllForRoom(int roomId)
        {
            List<RenovationRequestDisplayDto> renovationsDto = new List<RenovationRequestDisplayDto>();
            foreach (RenovationRequest renovationRequest in _renovationService.GetAllForRoom(roomId))
            {
                renovationsDto.Add(RenovationRequestDisplayMapper.EntityToEntityDto(renovationRequest));
            }
            return Ok(renovationsDto);
        }

        [HttpPost("decline")]
        public StatusCodeResult Decline([FromBody] int requestId)
        {
            if (_renovationService.GetById(requestId) == null) return NotFound();
            _renovationService.Decline(requestId);
            return Ok();
        }
    }
}
