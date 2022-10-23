namespace HospitalAPI.Controllers
{
    using HospitalLibrary.Core.DTO;
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Service;
    using HospitalLibrary.Core.Service.Core;
    using Microsoft.AspNetCore.Mvc;

    public class FloorController : BaseController<Floor>
    {
        private IFloorService _floorService;
        public FloorController(IFloorService floorService)
        {
            _floorService = floorService;
        }

        [HttpGet("detail/{id}")]
        public IActionResult GetDetails(int id)
        {
            FloorDetailsDTO entity = _floorService.GetDetails(id);
            if (entity == null)
            {
                return NotFound();
            }

            return Ok(entity);
        }
    }
}
