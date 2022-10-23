namespace HospitalAPI.Controllers
{
    using HospitalLibrary.Core.Model;
    using HospitalLibrary.Core.Service.Core;

    public class FloorController : BaseController<Floor>
    {
        private IFloorService _floorService;
        public FloorController(IFloorService floorService)
        {
            _floorService = floorService;
        }
    }
}
