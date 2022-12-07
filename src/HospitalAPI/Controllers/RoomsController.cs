using HospitalAPI.Dto;
using HospitalAPI.Mappers;
using HospitalLibrary.Core.DTO;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Service;
using HospitalLibrary.Core.Service.Core;
using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HospitalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : BaseController<Room>
    {
        private readonly IRoomService _roomService;

        public RoomsController(IRoomService roomService, IEquipmentService equipmentService)
        {
            _roomService = roomService;
        }

        [HttpPut]
        public IActionResult Update(RoomDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Bad request, please enter valid data.");
            }
            Room room = _roomService.GetById(dto.Id);
            if (room == null || room.Deleted)
            {
                return NotFound();
            }
            bool status = _roomService.Update(RoomMapper.EntityDtoToEntity(dto));
            if (status)
            {
                return Ok(room);
            }
            return BadRequest("Bad request, please enter valid data.");
        }

        [HttpPost]
        public IActionResult Search(SearchCriteriaDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Bad request, please enter valid data.");
            }
            List<Room> rooms = _roomService.Search(dto.RoomNumber, dto.FloorNumber, dto.BuildingId, dto.RoomPurpose, dto.WorkingHoursStart, dto.WorkingHoursEnd, dto.EquipmentType, dto.Quantity);
            List<RoomDto> searchedRoomsDto = new List<RoomDto>();
            if (rooms == null)
            {
                return NotFound();
            }

            rooms.ForEach(r => searchedRoomsDto.Add(RoomMapper.EntityToEntityDto(r)));

            return Ok(searchedRoomsDto);

        }

        [HttpGet("available")]
        public IActionResult GetAvailable()
        {
            List<Room> availableRooms = _roomService.GetAvailable().ToList();
            List<RoomDto> dtoList = new List<RoomDto>();

            if (availableRooms.IsNullOrEmpty())
            {
                return NotFound();
            }

            availableRooms.ForEach(r => dtoList.Add(RoomMapper.EntityToEntityDto(r)));
            return Ok(dtoList);
        }


        [HttpGet("all")]
        public IActionResult GetAll()
        {
            return Ok(_roomService.GetAll());
        }

        [HttpGet("workhour-rooms")]
        public IActionResult GetRoomsWithWorkingHour(int workHourId)
        {
            List<Room> rooms = _roomService.GetRoomsWithWorkingHour(workHourId).ToList();
            if (rooms.IsNullOrEmpty())
            {
                return NotFound();
            }
            List<RoomDto> dtoList = new List<RoomDto>();
            rooms.ForEach(r => dtoList.Add(RoomMapper.EntityToEntityDto(r)));
            return Ok(dtoList);
        }
    }
}
