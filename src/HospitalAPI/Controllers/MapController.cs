namespace HospitalAPI.Controllers
{
    using HospitalLibrary.Core.Service.Core;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class MapController : ControllerBase
    {
        private readonly IMapService _mapService;

        public MapController(IMapService mapService)
        {
            _mapService = mapService;
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            return Ok(_mapService.GetAll());
        }

    }
}
