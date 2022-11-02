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

        [HttpGet("getBuildings")]
        public IActionResult GetBuildings()
        {
            List<BuildingDto> buildingsDto = new List<BuildingDto>();
            List<Building> buildings = _mapService.GetBuildings().ToList();
            if (buildings == null)
            {
                return NotFound();
            }
            buildings.ForEach(r => buildingsDto.Add(BuildingMapper.EntityToEntityDto(r)));
            return Ok(buildingsDto);
        }

        [HttpGet("getRooms/{buildingId}")]
        public IActionResult GetBuildingRooms(int buildingId)
        {
            List<RoomMapDto> roomsMapDto = new List<RoomMapDto>();
            List<RoomMap> roomsMap = _mapService.GetBuildingRooms(buildingId).ToList();
            if (roomsMap == null)
            {
                return NotFound();
            }
            roomsMap.ForEach(r => roomsMapDto.Add(RoomMapMapper.EntityToEntityDto(r)));
            return Ok(roomsMapDto);
        }

        [HttpGet("getRooms/{buildingId}/{floor}")]
        public IActionResult GetFloorRooms(int buildingId, int floor)
        {
            List<RoomMapDto> roomsMapDto = new List<RoomMapDto>();
            List<RoomMap> roomsMap = _mapService.GetFloorRooms(buildingId, floor).ToList();
            if (roomsMap == null)
            {
                return NotFound();
            }
            roomsMap.ForEach(r => roomsMapDto.Add(RoomMapMapper.EntityToEntityDto(r)));
            return Ok(roomsMapDto);
        }

    }
}
