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
    public class BuildingsController : BaseController<Building>
    {
        private readonly IBuildingService _buildingService;

        public BuildingsController(IBuildingService buildingService)
        {
            _buildingService = buildingService;
        }

        [HttpPut]
        public IActionResult Update(BuildingDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Bad request, please enter valid data.");
            }
            Building building = _buildingService.Get(dto.Id);
            if (building == null || building.Deleted)
            {
                return NotFound();
            }
            Building status = _buildingService.Update(BuildingMapper.EntityDtoToEntity(dto));
            if (status != null)
            {
                return Ok(status);
            }
            return BadRequest("Bad request, please enter valid data.");
        }
    }
}
