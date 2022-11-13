namespace HospitalAPI.Controllers
{
    using HospitalLibrary.Core.DTO.BloodManagment;
    using HospitalLibrary.Core.Model.Blood.BloodManagment;
    using HospitalLibrary.Core.Service.Blood;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class BloodAcquisitionController : BaseController<BloodAcquisition>
    {
        private BloodAcquisitionService bloodAcquisitionService;

        
        public BloodAcquisitionController(BloodAcquisitionService _bloodAcquisitionService)
        {
            bloodAcquisitionService = _bloodAcquisitionService;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(bloodAcquisitionService.GetAll());
        }

        [HttpGet("/{id}")]
        public IActionResult Get(int id)
        {
            return Ok(bloodAcquisitionService.Get(id));
        }


        [HttpPost]
        public IActionResult Create(CreateAcquisitionDTO createAcquisitionDTO)
        {
            bloodAcquisitionService.Create(createAcquisitionDTO);
            return Ok();
        }

        

    }
}
