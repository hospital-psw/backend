using HospitalAPI.Dto;
using HospitalAPI.Mappers;
using HospitalLibrary.Core.DTO;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Service;
using HospitalLibrary.Core.Service.Core;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FloorsController : BaseController<Building>
    {
        private readonly IFloorService _floorService;

        public FloorsController(IFloorService floorService)
        {
            _floorService = floorService;
        }

        [HttpPut]
        public IActionResult Update(FloorDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Bad request, please enter valid data.");
            }
            Floor floor = _floorService.Get(dto.Id);
            if (floor == null || floor.Deleted)
            {
                return NotFound();
            }
            Floor status = _floorService.Update(FloorMapper.EntityDtoToEntity(dto));
            if (status != null)
            {
                return Ok(status);
            }
            return BadRequest("Bad request, please enter valid data.");
        }
    }
}
