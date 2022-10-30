using HospitalLibrary.Core.DTO;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Service;
using HospitalLibrary.Core.Service.Core;
using Microsoft.AspNetCore.Mvc;
using HospitalAPI.Dto;
using HospitalAPI.Mappers;

namespace HospitalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : BaseController<Room>
    {
        private readonly IRoomService _roomService;

        public RoomsController(IRoomService roomService)
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
                return Ok();
            }
            return BadRequest("Bad request, please enter valid data.");
        }

    }
}
