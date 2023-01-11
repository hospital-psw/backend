namespace HospitalAPI.Controllers
{
    using HospitalLibrary.Core.Model.Blood;
    using HospitalLibrary.Core.Service.AppUsers.Core;
    using HospitalLibrary.Core.Service.Blood.Core;
    using HospitalLibrary.Core.Service.Blood;
    using Microsoft.AspNetCore.Mvc;
    using HospitalLibrary.Core.Model.Blood.Enums;

    [ApiController]
    [Route("api/[controller]")]
    public class BloodAdditionController :  BaseController<BloodAddition>
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
