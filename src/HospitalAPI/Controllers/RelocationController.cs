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

        public OkObjectResult GetForRoom(int roomId)
        {
            throw new NotImplementedException();
        }

        [HttpPut("recommend")]
        public IActionResult GetRecommendedRelocationAppointments([FromBody] RecommendRelocationRequestDto dto)
        {
            return Ok(_relocationService.GetAppointments(dto.FromRoomId, dto.ToRoomId, dto.FromTime, dto.ToTime, dto.Duration));
        }
    }
}
