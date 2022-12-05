namespace HospitalAPI.Controllers
{
    using HospitalAPI.Dto;
    using HospitalAPI.Mappers;
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Service.Core;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
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
            try
            {
                RelocationRequest request = RelocationRequestMapper.EntityDtoToEntity(dto, _roomService.GetById(dto.FromRoomId), _roomService.GetById(dto.ToRoomId), _equipmentService.Get(dto.EquipmentId));
                return Ok(_relocationService.Create(request));
            }
            catch (Exception e) 
            {
                return BadRequest(e.Message);
            }
        }

        /*
        public OkObjectResult GetForRoom(int roomId)
        {
            throw new NotImplementedException();
        }
        */

        [HttpPut("recommend")]
        public IActionResult GetFreeTimeSlots([FromBody] RecommendRelocationRequestDto dto)
        {
            return Ok(_roomScheduleService.GetAppointments(dto.RoomsId, dto.FromTime, dto.ToTime, dto.Duration));
        }

        [HttpGet("{roomId}")]
        public IActionResult GetAllForRoom(int roomId)
        {
            List<RelocationRequestDisplayDto> relocationDtos = new List<RelocationRequestDisplayDto>();
            foreach (RelocationRequest relocationRequest in _relocationService.GetAllForRoom(roomId))
            {
                relocationDtos.Add(RelocationRequestDisplayMapper.EntityToEntityDto(relocationRequest));
            }
            return Ok(relocationDtos);
        }


    }
}
