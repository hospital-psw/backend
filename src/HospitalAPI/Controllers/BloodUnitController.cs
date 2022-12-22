namespace HospitalAPI.Controllers
{
    using HospitalLibrary.Core.DTO.BloodManagment;
    using HospitalLibrary.Core.Model.Blood;
    using HospitalLibrary.Core.Model.Blood.Enums;
    using HospitalLibrary.Core.Service.Blood.Core;
    using Microsoft.AspNetCore.Mvc;


    [ApiController]
    [Route("api/[controller]")]
    public class BloodUnitController : BaseController<BloodUnit>
    {
        private IBloodUnitService bloodUnitService;

        public BloodUnitController(IBloodUnitService _bloodUnitService)
        {
            bloodUnitService = _bloodUnitService;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(bloodUnitService.GetAll());
        }

        [HttpPost]
        public IActionResult Create(BloodUnit bloodUnit)
        {
            if(bloodUnitService.GetByBloodType(bloodUnit.BloodType) != null && bloodUnit.BloodType.GetType() != BloodType.AB_MINUS.GetType()) {
                return BadRequest("Blood type already exist");
            }


            return Ok(bloodUnitService.Add(bloodUnit));
        }

        [HttpGet("/{bloodType}")]
        public IActionResult GetAmountForSpecificBloodType(BloodType bloodType)
        {
            return Ok(bloodUnitService.GetAmountForSpecificBloodType(bloodType));
        }

        [HttpGet("get/{bloodType}")]
        public IActionResult GetByBloodType(BloodType bloodType)
        {
            return Ok(bloodUnitService.GetByBloodType(bloodType));
        }

        [HttpPut]
        public IActionResult UpdateBloodUnit(BloodUnitDTO dto)
        {
            return Ok(bloodUnitService.UpdateBloodUnit(dto));
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteBloodUnit(int id)
        {
            return Ok(bloodUnitService.Delete(id));
        }

    }
}
