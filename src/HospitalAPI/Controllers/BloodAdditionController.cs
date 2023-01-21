namespace HospitalAPI.Controllers
{
    using HospitalLibrary.Core.Model.Blood;
    using HospitalLibrary.Core.Model.Blood.Enums;
    using HospitalLibrary.Core.Service.AppUsers.Core;
    using HospitalLibrary.Core.Service.Blood;
    using HospitalLibrary.Core.Service.Blood.Core;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class BloodAdditionController : BaseController<BloodAddition>
    {

        private IBloodAdditionService bloodAdditionService;


        public BloodAdditionController(IBloodAdditionService _bloodAdditionService)
        {
            bloodAdditionService = _bloodAdditionService;

        }


        [HttpGet("{bloodType}")]
        public IActionResult GetByBloodType(BloodType bloodType)
        {

            return Ok(bloodAdditionService.GetByBloodType(bloodType));
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(bloodAdditionService.GetAll());
        }



    }
}
