namespace HospitalAPI.Controllers
{
    using HospitalLibrary.Core.DTO;
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Service.Core;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class BuildingController : BaseController<Building>
    {
        private IBuildingService _buildingService;
        public BuildingController(IBuildingService buildingService)
        {
            _buildingService = buildingService;
        }
        
        [HttpGet("detail/{id}")]
        public IActionResult GetBuildingDetails(int id)
        {
            BuildingDetailsDTO entity = _buildingService.GetBuildingDetails(id);
            if(entity == null)
            {
                return NotFound();
            }

            return Ok(entity);
        }
        
    }
}
