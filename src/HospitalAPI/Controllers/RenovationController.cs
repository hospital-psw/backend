namespace HospitalAPI.Controllers
{
    using HospitalAPI.Dto;
    using HospitalAPI.Mappers;
    using HospitalAPI.Mappers.Renovation;
    using HospitalLibrary.Core.Model;
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

        public RenovationController(IRenovationService renovationService, IRoomService roomService)
        {
            _renovationService = renovationService;
            _roomService = roomService;
        }

        [HttpPost("createRenovationRequest")]
        public IActionResult Create([FromBody] RenovationRequestDto dto)
        {
            List<Room> rooms = new List<Room>();
            foreach (int roomId in dto.RoomsId)
            {
                rooms.Add(_roomService.GetById(roomId));
            }
            return Ok(_renovationService.Create(RenovationRequestMapper.EntityDtoToEntity(dto, rooms)));
        }
    }
}
