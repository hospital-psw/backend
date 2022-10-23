using HospitalLibrary.Core.DTO;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Service;
using HospitalLibrary.Core.Service.Core;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomsController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            return Ok(_roomService.GetAll());
        }

        [HttpPost]
        public IActionResult Add(RoomDTO room)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (room == null)
            {
                return BadRequest();
            }

            Room response = _roomService.Add(room);

            return Ok(response);
        }

        [HttpGet("detail/{id}")]
        public IActionResult GetBuildingDetails(int id)
        {
            RoomDetailsDTO entity = _roomService.GetDetails(id);
            if (entity == null)
            {
                return NotFound();
            }

            return Ok(entity);
        }
    }
}