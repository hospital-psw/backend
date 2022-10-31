namespace HospitalAPI.Controllers
{
    using HospitalAPI.Dto;
    using HospitalAPI.Mappers;
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Service.Core;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;

    [Route("api/[controller]")]
    [ApiController]
    public class MapController : ControllerBase
    {
        private readonly IMapService _mapService;

        public MapController(IMapService mapService)
        {
            _mapService = mapService;
        }

        [HttpGet("getBuilding/{building}")]
        public IActionResult GetBuilding(string building)
        {
            List<RoomMapDto> roomsMapDto = new List<RoomMapDto>();
            List<RoomMap> roomsMap = _mapService.GetBuilding(building).ToList();
            if (roomsMap == null)
            {
                return NotFound();
            }
            roomsMap.ForEach(r => roomsMapDto.Add(RoomMapMapper.EntityToEntityDto(r)));
            return Ok(roomsMapDto);
        }

        [HttpGet("getFloor/{building}/{floor}")]
        public IActionResult GetFloor(string building, int floor)
        {
            List<RoomMapDto> roomsMapDto = new List<RoomMapDto>();
            List<RoomMap> roomsMap = _mapService.GetFloor(building, floor).ToList();
            if (roomsMap == null)
            {
                return NotFound();
            }
            roomsMap.ForEach(r => roomsMapDto.Add(RoomMapMapper.EntityToEntityDto(r)));
            return Ok(roomsMapDto);
        }

    }
}
